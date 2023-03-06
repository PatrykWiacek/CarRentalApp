﻿using CarRental.DAL.Models;
using CarRental.Web.Models;
using CarRental.Logic.Services;
using CarRental.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Web.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService) 
        {
            this._carService = carService;
        }

        // GET: CarController
        public IActionResult Index()
        {
            var cars = this._carService.GetAll();
            var mapper = new CarMapper();
            var model = mapper.Map(cars);
            return View(model);
        }

        // GET: CarController/Details/5
        public IActionResult Details(int id)
        {
            var model = _carService.GetById(id);
            return View(model);
        }

        // GET: CarController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Car model)
        {
            try
            {
                _carService.Create(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: CarController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: CarController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(SearchViewModel vm)
        {
            var cars = _carService.SearchList(vm.Search);
            return View(cars);
        }
        
    }
}
