using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.Test.Models;

namespace CarRental.Test.MapperProfiles;

public class RentalProfile : Profile
{
    public RentalProfile()
    {
        CreateMap<Rental, RentalViewModel>(MemberList.Destination);

        CreateMap<RentalViewModel, Rental>()
            .ForSourceMember(r => r.CustomerName, opt => opt.DoNotValidate())
            .ForSourceMember(r => r.CarLicencePlate, opt => opt.DoNotValidate());

    }
}
