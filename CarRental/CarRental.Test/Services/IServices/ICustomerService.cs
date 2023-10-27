﻿using CarRental.Test.Models;

namespace CarRental.Test.Services.IServices;

public interface ICustomerService
{
    IEnumerable<CustomerViewModel> GetAll();
    CustomerViewModel? Get(int id);
    Task CreateAsync(CustomerViewModel model);
    void Update(CustomerViewModel customer);
    void Delete(int id);
}
