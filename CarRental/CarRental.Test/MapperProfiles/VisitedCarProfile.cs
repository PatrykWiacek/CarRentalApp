using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.Test.Models;

namespace CarRental.Test.MapperProfiles;
public class VisitedCarProfile : Profile
{
    public VisitedCarProfile()
    {
        CreateMap<VisitedCar, VisitedCarViewModel>()
            .ReverseMap();
    }
}
