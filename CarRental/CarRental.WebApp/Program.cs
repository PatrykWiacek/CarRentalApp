using System.Net.Http.Json;
using CarRental.DAL.Context;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories;
using CarRental.Logic.MailConf;
using CarRental.Logic.MapperProfiles;
using CarRental.Logic.Models;
using CarRental.Logic.Services;
using CarRental.Logic.Services.IServices;
using CarRental.Logic.ServicesApi;
using CarRental.Logic.ServicesApi.IServiceApi;
using CarRental.Logic.Validators;
using CarRental.WebApp;
using FluentValidation;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICarService, CarService>(); 
builder.Services.AddScoped<IRentalService, RentalService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ICarApiService, CarApiService>();
builder.Services.AddAutoMapper(typeof(CustomerProfile));

// Validation
builder.Services.AddScoped<IValidator<CustomerViewModel>, CustomerViewModelValidator>();
builder.Services.AddScoped<IValidator<CarViewModel>, CarViewModelValidator>();
builder.Services.AddScoped<IValidator<RentalViewModel>, RentalViewModelValidator>();

builder.Services.AddScoped<UserManager<Customer>>();
builder.Services.AddScoped<RoleManager<IdentityRole<int>>>();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddHttpClient<ICarService, CarService>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
//builder.Services.AddHttpClient("WebApi", sp => sp.BaseAddress = new Uri("https://localhost:7225/api/Car"));
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("WebApi"));



await builder.Build().RunAsync();
