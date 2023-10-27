using CarRental.Logic.Models;
using CarRental.Logic.Services.IServices;
using Microsoft.AspNetCore.Components;

namespace CarRental.WebApp.Pages;

public partial class Home
{
    [Inject]
    public ICarService? CarService { get; set; }
    public SearchFieldsModel SearchDto { get; set; } = new SearchFieldsModel();
    private List<CarViewModel> Cars { get; set; } = default!;

    protected override void OnInitialized()
    {
        Cars = ( CarService.GetAll()).ToList();
    }
}
