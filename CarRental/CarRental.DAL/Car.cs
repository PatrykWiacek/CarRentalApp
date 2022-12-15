﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DAL
{
    internal class Car
    {
        public string Model { get; set; }
        public string Make { get; set; }
        public int Year { get; set; }
        public string Color { get; set;}
        public bool Ac { get; set; }
        public string Transmission { get; set; }
        public string FuelType { get; set; }
        public int MaxCapacity { get; set; }
        public string Addons { get; set; }

    }
}
