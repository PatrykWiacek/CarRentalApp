
using CarRental.Logic.Models;

namespace CarRental.Logic.ServicesApi.IServiceApi;
public interface IRentalApiService
{
    Task<IEnumerable<RentalViewModel>> GetAll();
    Task<RentalViewModel> Get(int id);
    Task Add(RentalViewModel model);
    Task Update(RentalViewModel model);
    Task Delete(int id);
}
