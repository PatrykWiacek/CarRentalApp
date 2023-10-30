using CarRental.Logic.Models;
using CarRental.Logic.Services;
using CarRental.Logic.Services.IServices;
using CarRental.Logic.ServicesApi.IServiceApi;
using Microsoft.AspNetCore.Components;

namespace CarRental.WebApp.Pages;

public partial class Home
{
    [Inject]
    public ICarApiService? CarApiService { get; set; }

    public string Search { get; set; } = default!;
    private IEnumerable<CarViewModel> Cars { get; set; } = new List<CarViewModel>();

    protected override async Task OnInitializedAsync()
    {
        Cars =  await ( CarApiService.GetAllCarsAsync());
    }
}
