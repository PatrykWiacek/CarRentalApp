﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CarRental.DAL.Entities.BaseEntity;

namespace CarRental.DAL.Entities;
public class LastLoggedReport : Entity
{
    public int? UserId { get; set; }
    public DateTime LastLogged { get; set; }
    public int LoginCount { get; set; }
}
