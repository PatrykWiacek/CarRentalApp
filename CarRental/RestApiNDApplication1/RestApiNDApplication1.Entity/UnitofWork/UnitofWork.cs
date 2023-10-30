using RestApiNDApplication1.Entity.Context;
using RestApiNDApplication1.Entity.Queries;
using RestApiNDApplication1.Entity.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace RestApiNDApplication1.Entity.UnitofWork;
public interface IUnitOfWork : IDisposable
{

    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class;

    RestApiNDApplication1Context Context { get; }
    void Save();
    Task SaveAsync();
}

public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : IDbConnectionProvider
{
}

public class UnitOfWork : IUnitOfWork
{
    public RestApiNDApplication1Context Context { get; }

    private Dictionary<Type, object> _repositoriesAsync;
    private Dictionary<Type, object> _repositories;
    private bool _disposed;

    public UnitOfWork(RestApiNDApplication1Context context)
    {
        Context = context;
        _disposed = false;
    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        if (_repositories == null) _repositories = new Dictionary<Type, object>();
        var type = typeof(TEntity);
        if (!_repositories.ContainsKey(type)) _repositories[type] = new Repository<TEntity>(Context);
        return (IRepository<TEntity>)_repositories[type];
    }

    public IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class
    {
        if (_repositories == null) _repositoriesAsync = new Dictionary<Type, object>();
        var type = typeof(TEntity);
        if (!_repositoriesAsync.ContainsKey(type)) _repositoriesAsync[type] = new RepositoryAsync<TEntity>(Context);
        return (IRepositoryAsync<TEntity>)_repositoriesAsync[type];
    }

    public void Save()
    {
        if (Context.Transaction != null)
            Context.Transaction.Commit();
    }

    public async Task SaveAsync()
    {
        await Task.Run(() =>
        {
            if (Context.Transaction != null)
                Context.Transaction.Commit();
        });
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    public void Dispose(bool isDisposing)
    {
        if (!_disposed)
        {
            if (isDisposing)
            {
                if (Context.Connection != null) Context.Connection.Dispose();
                if (Context.Transaction != null) Context.Transaction.Dispose();
            }
        }
        _disposed = true;
    }
}
