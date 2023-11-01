using AutoMapper;
using CarRental.DAL.Context;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories;
using CarRental.Logic.Models;
using CarRental.Logic.ServicesApi.IServiceApi;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Logic.ServicesApi;
public class RentalApiService : IRentalApiService
{
    private readonly IRepository<Rental> _RentalRepository;
    private readonly IMapper _mapper;
    private readonly ApplicationContext _applicationContext;

    public RentalApiService(IMapper mapper, ApplicationContext applicationContext)
    {
        _mapper = mapper;
        _applicationContext = applicationContext;
    }

    public async Task<IEnumerable<RentalViewModel>> GetAll()
    {
        IEnumerable<Rental> accesses = await _applicationContext.Rentals.ToListAsync();
        return _mapper.Map<List<RentalViewModel>>(accesses); ;
    }

    public async Task<RentalViewModel> Get(int id)
    {
        var access = await _applicationContext.Rentals.FirstOrDefaultAsync(r => r.Id == id);
        return _mapper.Map<RentalViewModel>(access);
    }

    public async Task Add(RentalViewModel model)
    {
        var access = _mapper.Map<Rental>(model);
        await _applicationContext.Rentals.AddAsync(access);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task Update(RentalViewModel model)
    {
        var access = _mapper.Map<Rental>(model); 
        _applicationContext.Rentals.Update(access);
    }

    public async Task Delete(int id)
    {
        var guest = await _applicationContext.Rentals.FirstOrDefaultAsync(x => x.Id == id);
        _applicationContext.Rentals.Update(guest); 
        await _applicationContext.SaveChangesAsync();

    }
}
