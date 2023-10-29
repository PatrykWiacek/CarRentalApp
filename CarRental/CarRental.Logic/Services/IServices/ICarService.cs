using CarRental.Logic.Models;

namespace CarRental.Logic.Services.IServices;

public interface ICarService
{
    Task<IEnumerable<CarViewModel>> GetAll();
    Task<CarViewModel>? Get(int id);
    Task<IEnumerable<CarViewModel>> FindCars(IEnumerable<CarViewModel> collection, SearchFieldsModel sfModel);
    void Create(CarViewModel car);
    Task Update(CarViewModel car);
    Task Delete(int id);
}
