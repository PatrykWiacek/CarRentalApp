﻿using CarRental.Common.Enums;
using CarRental.DAL.Entities.BaseEntity;

namespace CarRental.DAL.Entities;

public class Customer : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string? EmailAddress { get; set; }
    public string? Pesel { get; set; }
    public Gender Gender { get; set; }
}
