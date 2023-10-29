using CarRental.Logic.Models;
using CarRental.Logic.Services.IServices;
using Microsoft.AspNetCore.Components;

namespace CarRental.WebApp.Components;

public partial class HomeSearch
{
    [Inject]
    public ICarService? CarService { get; set; }
    private SearchFieldsModel SearchDto { get; set; } = default!;
    private List<CarViewModel> Cars { get; set; } = default!;

}
