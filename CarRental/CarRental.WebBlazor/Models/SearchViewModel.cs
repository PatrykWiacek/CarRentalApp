namespace CarRental.Test.Models;

public class SearchViewModel
{
    public SearchFieldsModel SearchDto { get; set; } = new SearchFieldsModel();
    public IEnumerable<CarViewModel> Cars { get; set; } = new List<CarViewModel>();
}
