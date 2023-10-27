using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.Test.Models;

namespace CarRental.Test.MapperProfiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<CustomerViewModel, Customer>(MemberList.Source);

        CreateMap<Customer, CustomerViewModel>(MemberList.Destination);
    }
}
