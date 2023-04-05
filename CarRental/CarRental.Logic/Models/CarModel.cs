﻿using System.ComponentModel.DataAnnotations;
using CommonLibrary.Enums;

namespace CarRental.Logic.Models;
public sealed class CarModel
{
    public CarModel() { }

    #region Properties/Fields

    public int Id { get; set; }

    [Display(Name = "Car Model")]
    public string CarModelProp { get; set; }
    public string Make { get; set; }
    public int Year { get; set; }
    public string? Color { get; set; }
    public TransmissionType? Transmission { get; set; }
    public string LicencePlateNumber { get; set; }
    public int? Kilometrage { get; set; }
    public float? PowerInKiloWats { get; set; }
    public string? EngineType { get; set; }
    public string? Displacement { get; set; } // ex. 1.8, 1.5 T-GDI, etc.
    public int? Doors { get; set; }
    public int? SeatsNo { get; set; } // total number of seats (with driver seat included)
    public int? Airbags { get; set; }
    public string? FuelConsumption { get; set; } // in l/100km format city/highway, ex. "6.5/4.5"
    public List<string> Addons { get; set; } = new();
    public decimal? Price { get; set; }

    private static List<string> _availableAddons = new() { "Ac", "towbar", "ABS", "roof rack" };

    public static List<string> GetAvailableAddons() { return _availableAddons; }

    #endregion

    public CarModel(string make, string model, string licensePlate)
    {
        Make = make;
        CarModelProp = model;
        LicencePlateNumber = licensePlate;
    }
}
