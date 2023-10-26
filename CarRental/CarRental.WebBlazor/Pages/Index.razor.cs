using CarRental.Logic.Models;
using Microsoft.AspNetCore.Components;

namespace CarRental.WebBlazor.Pages;

public partial class Index
{
    public SearchFieldsModel SearchDto { get; set; } = new SearchFieldsModel();
    public IEnumerable<CarViewModel> Cars { get; set; } = new List<CarViewModel>();
}
