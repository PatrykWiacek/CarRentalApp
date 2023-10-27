using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.Test.Models;

namespace CarRental.Test.MapperProfiles;

public class CarProfile : Profile
{
    public CarProfile()
    {
        CreateMap<Car, CarViewModel>()
            .ReverseMap();
    }
}
