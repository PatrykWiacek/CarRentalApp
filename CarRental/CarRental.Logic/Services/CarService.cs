﻿using CarRental.DAL;
using CarRental.Logic.Services.IServices;
using CarRental.Logic.Model;

namespace CarRental.Logic.Services;

public class CarService : ICarService
{
    private static int _idCounter = CarRentalData.Cars.Max(c => c.Id);
    private List<CarModel> _cars = CarRentalData.Cars;

    public IEnumerable<CarModel> GetAll()
    {
        return CarRentalData.Cars;
    }

    public IEnumerable<CarModel> GetByName(string name)
    {
        List<CarModel> cars = new();
        if (string.IsNullOrEmpty(name))
        {
            cars = GetAll().ToList();
        }
        else
        {
            cars = CarRentalData.Cars.Where(c => c.Make.Contains(name, StringComparison.CurrentCultureIgnoreCase)
                || c.CarModel.Contains(name, StringComparison.CurrentCultureIgnoreCase)
            ).ToList();
        }
        return cars;
    }

    public List<CarModel> GetByYear(string read)
    {
        int year;
        bool makes = int.TryParse(read, out year);
        List<CarModel> cars = new();
        if (read == null)
        {
            cars = GetAll().ToList();
        }
        else
        {
            cars = GetAll().Where(c => c.Year == year || c.CarModel.ToLower().Contains(read.ToLower())).ToList();
        }
        return cars;
    }

    public List<CarModel> GetByAddons(string addon)
    {
        List<CarModel> cars = new List<CarModel>();
        if (string.IsNullOrEmpty(addon))
        {
            cars = CarRentalData.Cars;
        }
        else
        {
            foreach (var car in CarRentalData.Cars)
            {
                foreach (var item in car.Addons)
                {
                    if (item.Contains(addon))
                    {
                        cars.Add(car);
                        break;
                    }
                }
            }

        }
        return cars;
    }

    public void Create(CarModel car)
    {
        car.Id = GetNextId();
        CarRentalData.Cars.Add(car);
    }

    public CarModel? GetById(int carId)
    {
        return CarRentalData.Cars.FirstOrDefault(c => c.Id == carId);
    }

    public void Delete(int carId)
    {
        var car = GetById(carId);
        _cars.Remove(car);
    }

    private int GetNextId()
    {
        return ++_idCounter;
    }

    public void Update(CarModel model)
    {
        var car = GetById(model.Id);

        car.CarModel = model.CarModel;
        car.Make = model.Make;
        car.Kilometrage = model.Kilometrage;
        car.Year = model.Year;
        car.Airbags = model.Airbags;
        car.Addons = model.Addons;
        car.Color = model.Color;
        car.Doors = model.Doors;
        car.Displacement = model.Displacement;
        car.EngineType = model.EngineType;
        car.FuelConsumption = model.FuelConsumption;
        car.LicencePlateNumber = model.LicencePlateNumber;
        car.SeatsNo = model.SeatsNo;
        car.PowerInKiloWats = model.PowerInKiloWats;
        car.Price = model.Price;
        car.Transmission = model.Transmission;
    }
}
