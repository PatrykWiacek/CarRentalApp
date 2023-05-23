﻿using System.Text;
using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.Logic.Models;
using CarRental.Logic.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CarRental.Logic.Services;
public class ReportService : IReportService // ReportService
{
    private readonly HttpClient httpClient;
    private readonly IMapper _mapper;
    private readonly UserManager<Customer> _userManager;
    public ReportService(IMapper mapper, UserManager<Customer> userManager)
    {
        httpClient = new HttpClient();
        _mapper = mapper;
        _userManager = userManager;
    }

    private async Task<HttpResponseMessage> PostUserActivityAsync(object reportModel, string apiEndpoint)
    {
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(reportModel);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        return await httpClient.PostAsync(apiEndpoint, content);
    }

    public async Task ReportCarVisitAsync(CarViewModel visitedCar, string userId)
    {
        int userIdToInt;
        userIdToInt = int.Parse(userId);
        var apiEndpoint = "https://localhost:7225/VisitedCar";
        var carToPost = new VisitedCarDTO
        {
            UserId = userIdToInt,
            CarId = visitedCar.Id,
            DateWhenClicked = DateTime.Now,
            Make = visitedCar.Make,
            Model = visitedCar.CarModelProp,
            Year = visitedCar.Year,
            LicencePlate = visitedCar.LicencePlateNumber
        };

        _mapper.Map<VisitedCar>(carToPost);
        var response = await PostUserActivityAsync(carToPost, apiEndpoint);
        if (!response.IsSuccessStatusCode)
        {
            // Task logowanie zdarzeń aplikacji (dodać log.Error)
        }
    }

    public async Task<int> GetUserIdAsync(string email)
    {
        var user = _userManager.Users.FirstOrDefault(c => c.Email == email);
        var userId = user.Id;
        return userId;
    }
    public async Task ReportUserLoginAsync(int userId)
    {

        var apiEndpoint = "https://localhost:7225/Report";
        var userToPost = new LastLoggedReportDTO
        {
            UserId = userId,
            LastLogged = DateTime.Now,
            LoginCount = +1
        };

        _mapper.Map<LastLoggedReport>(userToPost);
        var response = await PostUserActivityAsync(userToPost, apiEndpoint);
        if (!response.IsSuccessStatusCode)
        {
            // Task logowanie zdarzeń aplikacji (dodać log.Error)
        }
    }
}
