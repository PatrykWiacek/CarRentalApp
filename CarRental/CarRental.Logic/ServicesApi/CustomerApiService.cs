using AutoMapper;
using CarRental.DAL.Context;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories;
using CarRental.Logic.Models;
using CarRental.Logic.ServicesApi.IServiceApi;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Logic.ServicesApi;
public class CustomerApiService : ICustomerApiService
{
    private readonly IMapper _mapper;
    private readonly ApplicationContext _applicationContext;

    public CustomerApiService(IMapper mapper, ApplicationContext applicationContext)
    {
        _mapper = mapper;
        _applicationContext = applicationContext;
    }

    public async Task<IEnumerable<CustomerViewModel>> GetAll()
    {
        IEnumerable<Customer> customers = await _applicationContext.Customers.ToListAsync();
        return _mapper.Map<List<CustomerViewModel>>(customers); ;
    }

    public async Task<CustomerViewModel> Get(int id)
    {
        var access = await _applicationContext.Customers.FindAsync(id);
        return _mapper.Map<CustomerViewModel>(access);
    }

    public async Task Add(CustomerViewModel model)
    {
        var access = _mapper.Map<Customer>(model);
        await _applicationContext.Customers.AddAsync(access);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task Update(CustomerViewModel model)
    {
        var access = _mapper.Map<Customer>(model); 
        _applicationContext.Customers.Update(access);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var guest = await _applicationContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
        _applicationContext.Customers.Remove(guest);
        await _applicationContext.SaveChangesAsync();
    }
}
