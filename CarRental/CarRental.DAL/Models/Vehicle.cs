﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DAL.Models
{
    public abstract class Vehicle
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("model")]
        public string? Model { get; set; }

        [JsonProperty("make")]
        public string? Make { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; } // production year

        [JsonProperty("color")]
        public string? Color { get; set; }

        [JsonProperty("transmission")]
        public string? Transmission { get; set; }

        public string? VIN { get; set; } // Vehicle Identification Number
        [JsonProperty("licence_plate_number")]
        public string? LicencePlateNumber { get; set; } // numer rejestracyjny pojazdu

        [JsonProperty("images")]
        public Dictionary<string, string> Images = new Dictionary<string, string>(); // dictionary for paths to car images

        [JsonProperty("kilometrage")]
        public int Kilometrage { get; set; }

        [JsonProperty("engine_parameters")]
        public EngineParameters EngineParameters { get; set; }

        public override string ToString()
        {
            return $"{GetType()}: {Model} {Make} {LicencePlateNumber} (id:{Id})";
        }
    }
}
