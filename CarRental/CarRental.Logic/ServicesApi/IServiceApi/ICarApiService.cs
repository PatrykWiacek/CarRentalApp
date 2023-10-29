using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Logic.Models;

namespace CarRental.Logic.ServicesApi.IServiceApi;
public interface ICarApiService
{
    Task<IEnumerable<CarViewModel>> GetAllCarsAsync();
}
