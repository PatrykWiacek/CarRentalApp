﻿using CarRental.DAL;
using CarRental.DAL.Models;
using CarRental.DAL.Utilities;

namespace CarRental.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //DataReaderAndWriter.WriteCarsToJson(@"..\..\..\..\CarRental.DAL\Data\cars2.json");

            List<Car> cars = DataReaderAndWriter.GetCarsFromJson();
            //Console.WriteLine($"Number of cars in database: {cars.Count}");
            //foreach (Car car in cars)
            //{
            //    Console.WriteLine(car.Model);
            //}
            Console.Title = "Cud Auta";
            ConsoleMenu.Menu();
        }
        public static void PrintCarList(List<Car> cars)
        {
            Console.WriteLine($"Number of cars in database: {cars.Count}");
            foreach (Car car in cars)
            {
                //Console.WriteLine(car.Model);
                car.PrintDetails();
            }
        }
    }
}