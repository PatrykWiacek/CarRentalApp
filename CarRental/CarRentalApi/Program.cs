using CarRental.DAL.Context;
using Microsoft.EntityFrameworkCore;
using CarRental.Logic.MapperProfiles;
using CarRental.DAL.Repositories;
using CarRental.Logic.Models;
using CarRental.Logic.Services;
using CarRental.Logic.Services.IServices;
using CarRental.Logic.ServicesApi;
using CarRental.Logic.ServicesApi.IServiceApi;
using CarRental.Logic.Validators;
using FluentValidation;
using CarRental.DAL.Entities;
using CarRental.Logic.MailConf;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration
    .GetConnectionString("DefaultConnection"))
);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IReportApiService, ReportApiService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddAutoMapper(typeof(CustomerProfile));

// Validation
builder.Services.AddScoped<IValidator<CarViewModel>, CarViewModelValidator>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
