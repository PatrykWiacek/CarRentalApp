using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.Test.Models;

namespace CarRental.Test.MapperProfiles;
public class LastLoggedProfile : Profile
{
    public LastLoggedProfile()
    {
        CreateMap<LastLoggedReport, LastLoggedReportDTO>()
            .ReverseMap();
    }
}
