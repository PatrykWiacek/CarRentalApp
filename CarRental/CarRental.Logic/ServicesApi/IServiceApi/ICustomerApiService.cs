using CarRental.Logic.Models;

namespace CarRental.Logic.ServicesApi.IServiceApi;
public interface ICustomerApiService
{
    Task<IEnumerable<CustomerViewModel>> GetAll();
    Task<CustomerViewModel> Get(int id);
    Task Add(CustomerViewModel model);
    Task Update(CustomerViewModel model);
    Task Delete(int id);
}
