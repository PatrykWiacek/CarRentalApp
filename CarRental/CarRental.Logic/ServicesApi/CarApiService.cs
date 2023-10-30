using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using CarRental.DAL.Entities;
using CarRental.Logic.Models;
using CarRental.Logic.Services.IServices;
using CarRental.Logic.ServicesApi.IServiceApi;
using Newtonsoft.Json;

namespace CarRental.Logic.ServicesApi;
public class CarApiService : ICarApiService
{
    private readonly HttpClient _httpClient;

    public CarApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<CarViewModel>> GetAllCarsAsync()
    {
        try
        {

            var apiEndpoint = "https://localhost:7225/api/Car";
            var respone = await _httpClient.GetFromJsonAsync<IEnumerable<CarViewModel>>(apiEndpoint);
            if (respone != null)
            {
                return respone;
            }

            return Enumerable.Empty<CarViewModel>();
        }
        catch (HttpRequestException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        
    }

    
}
