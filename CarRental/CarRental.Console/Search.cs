﻿using CarRental.DAL;
using CarRental.DAL.Models;
using CarRental.Logic;

namespace CarRental.ConsoleUI;
public class Search
{
    public static void CarByMake()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Podaj nazwe auta:");
            var make = Console.ReadLine();
            var cars = LogicSearch.CarByMake(make);
            Print(cars);
            ConsoleKeyInfo read = Console.ReadKey();
            if (read.Key == ConsoleKey.Escape) break;
        }
    }
 
    
    public static void CarByProductionYear()
    {
        while (true)
        {
            Console.WriteLine("Podaj rok auta:");
            List<Car> cars = new List<Car>();
            var make = int.Parse(Console.ReadLine());
            if (make == null)
            {
                cars = CarRentalData.Cars;
            }
            else
            {
                cars = CarRentalData.Cars.Where(c => c.Year == make).ToList();
            }

            Print(cars);
            ConsoleKeyInfo read = Console.ReadKey();
            if (read.Key == ConsoleKey.Escape) break;
        }
    }

    public static void CarByAddon()
    {
        while (true)
        {
            Console.WriteLine("Podaj jakie wyposażenie cie interesuje:");
            var addon = Console.ReadLine();
            var cars = LogicSearch.CarByAddons(addon);
            Print(cars);
            ConsoleKeyInfo read = Console.ReadKey();
            if (read.Key == ConsoleKey.Escape) break;
        }
    }

        public static void PrintDetails(List<Car> cars)
        {
            if (cars is null || cars.Count == 0)
            {
                  Console.WriteLine("Brak samochodów o tych parametrach");
            }
            else
            {
                foreach (var car in cars)
                {
                    Console.WriteLine(car.GetDetails());
                }
            }
        }
    public static void Print(List<Car> cars)
    {
        if (cars is null || cars.Count == 0)
        {
            Console.WriteLine("Brak samochodów o tych parametrach");
        }
        else
        {
            foreach (var car in cars)
            {
                Console.WriteLine(car.ToString());
            }
        }
    }
    //Zastępstwo za przyszłe funkcje
    public static void PlaceHolder()
    {
        Console.WriteLine("PlaceHolder");
        Console.ReadKey();
    }
}
