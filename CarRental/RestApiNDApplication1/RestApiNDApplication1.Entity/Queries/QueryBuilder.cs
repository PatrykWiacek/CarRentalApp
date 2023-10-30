using Dapper;
using RestApiNDApplication1.Entity.Context;
using RestApiNDApplication1.Entity.Queries;
using RestApiNDApplication1.Entity.UnitofWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RestApiNDApplication1.Entity.Queries;
public partial class QueryBuilder
{

    private IQuery _query;
    public IQuery Build(Type type)
    {
        switch (type.ToString())
        {
            case "RestApiNDApplication1.Entity.Account":
                _query = new AccountQuery();
                break;
            case "RestApiNDApplication1.Entity.User":
                _query = new UserQuery();
                break;
            default:
                break;
        }
        if (_query == null)
            SearchAdditionalEntityTypes(type, ref _query);
        return _query;
    }

    //extend for additional entity types checks for T4 code 
    partial void SearchAdditionalEntityTypes(Type type, ref IQuery query);

}
