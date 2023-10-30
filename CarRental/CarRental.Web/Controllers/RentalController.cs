using CarRental.Logic.Models;
using CarRental.Logic.Services.IServices;
using CarRental.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Web.Controllers;

public class RentalController : Controller
{
    private readonly IRentalService _rentalService;
    private readonly ICustomerService _customerService;
    private readonly ICarService _carService;

    public RentalController(IRentalService rentalService, ICustomerService customerService, ICarService carService)
    {
        _customerService = customerService;
        _carService = carService;
        _rentalService = rentalService;
    }
    // GET: RentalConroller
    public async Task<IActionResult> Index()
    {
        var rentals = _rentalService.GetAll();

        List<RentalViewModel> model = new();

        foreach (var rental in rentals)
        {
            var customerName = _customerService?.Get(rental.CustomerId)?.FirstName
                + " " + _customerService?.Get(rental.CustomerId)?.LastName;

            var carLicencePlate = await _carService?.Get(rental.CarId);

            model.Add(new RentalViewModel
            {
                Id = rental.Id,
                CarId = rental.CarId,
                CustomerId = rental.CustomerId,
                BeginDate = rental.BeginDate,
                EndDate = rental.EndDate,
                TotalCost = rental.TotalCost,
                CustomerName = customerName,
            });
        }
        return View(model);
    }

    // GET: RentalConroller/Details/5
    public IActionResult Details(int id)
    {
        var rental = _rentalService.Get(id);
        dynamic d = GetShortCustomers().FirstOrDefault<object>(rental.CustomerId);
        object shortCustomer = d.FirstName + " " + d.LastName;

        var carLicencePlate = _carService?.Get(rental.CarId);

        var rentalViewModel = new RentalViewModel
        {
            Id = rental.Id,
            CarId = rental.CarId,
            CustomerId = rental.CustomerId,
            BeginDate = rental.BeginDate,
            EndDate = rental.EndDate,
            TotalCost = rental.TotalCost,

            CustomerName = shortCustomer.ToString(),
        };

        return View(rentalViewModel);
    }

    // GET: RentalConroller/Create
    [Authorize]
    public async Task<IActionResult> Create(CarViewModel car)
    {
        var carToRent = await _carService?.Get(car.Id);
        var temp = DateTime.Now;
        var beginDate = new DateTime(temp.Year, temp.Month, temp.Day, temp.Hour, temp.Minute, 0);

        var model = new RentalCreateViewModel
        {
            Car = carToRent,
            BeginDate = beginDate,
            EndDate = beginDate.AddDays(1),
            TotalCost = 0m,
            CarId = car.Id
        };

        return View(model);
    }

    // POST: RentalConroller/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RentalCreateViewModel model)
    {
        var carToRent = _carService?.Get(model.CarId);

        try
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var rentalModel = new RentalViewModel
            {
                CarId = model.CarId,
                CustomerId = model.CustomerId,
                BeginDate = model.BeginDate,
                EndDate = model.EndDate,
                TotalCost = model.TotalCost,
                Car = await carToRent,
            };

            decimal carPricePerDay = (decimal)_carService!.Get(model.Car.Id)!.AsyncState;
            rentalModel.TotalCost = _rentalService.GetRentalTotalPrice(carPricePerDay, rentalModel.BeginDate, rentalModel.EndDate);

            _rentalService.Create(rentalModel);
            TempData["AlertText"] = "Rental created successfully";
            TempData["AlertClass"] = AlertType.Success;

            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            TempData["AlertText"] = "Some error occured. Rental was not created";
            TempData["AlertClass"] = AlertType.Warning;
            return View(model);
            //return RedirectToAction(nameof(Index));
        }
    }

    // GET: RentalConroller/Edit/5
    public IActionResult Edit(int id)
    {
        var rentalModel = _rentalService.Get(id);
        var shortCustomers = GetShortCustomers();
        var shortCars = GetShortCars();
        var temp = DateTime.Now;
        var b = rentalModel.BeginDate;
        var e = rentalModel.EndDate;
        var beginDate = new DateTime(b.Year, b.Month, b.Day, b.Hour, b.Minute, 0);
        var endDate = new DateTime(e.Year, e.Month, e.Day, e.Hour, e.Minute, 0);

        var model = new RentalCreateViewModel
        {
            Id = rentalModel.Id,
            CarId = rentalModel.CarId,
            CustomerId = rentalModel.CustomerId,
            BeginDate = beginDate,
            EndDate = endDate,
            TotalCost = (decimal)rentalModel.TotalCost,

            Customers = shortCustomers,
        };
        return View(model);
    }

    // POST: RentalConroller/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(RentalViewModel model)
    {
        try
        {
            _rentalService.Update(model);

            TempData["AlertText"] = "Rental updated successfully";
            TempData["AlertClass"] = AlertType.Success;
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: RentalConroller/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var rental = _rentalService.Get(id);
        var car = await _carService.Get(rental.CarId);
        var customer = _customerService.Get(rental.CustomerId);

        TempData["AlertText"] = "You are in danger zone";
        TempData["AlertClass"] = AlertType.Warning;
        ViewData["Customer"] = customer.FirstName + " " + customer.LastName;
        ViewData["Car"] = car.Make + " " + car.CarModelProp + ", " + car.LicencePlateNumber;

        return View(rental);
    }

    // POST: RentalConroller/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            _rentalService.Delete(id);
            TempData["AlertText"] = "Rental deleted successfully";
            TempData["AlertClass"] = AlertType.Success;
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    private List<object> GetShortCustomers()
    {
        return _customerService.GetAll().Select(x => new { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName }).ToList<object>();
    }

    private async Task<List<object>> GetShortCars()
    {
        var cars = await _carService.GetAll();
        return cars.Select(x => new { x.Id, x.LicencePlateNumber, x.Make, x.CarModelProp }).ToList<object>();
    }
    public async Task<decimal> GetTotalCost(int carId, DateTime? beginDate = null, DateTime? endDate = null)
    {
        if (beginDate == null || endDate == null)
        {
            return 0m;
        }

        decimal? carPricePerDay;

        try
        {
            var car = await _carService.Get(carId);
            carPricePerDay = car.Price;
        }
        catch (Exception)
        {
            carPricePerDay = 0m;
        }
        var total = _rentalService.GetRentalTotalPrice((decimal)carPricePerDay, (DateTime)beginDate, (DateTime)endDate);
        return Math.Round(total, 2, MidpointRounding.ToZero);
    }
}
