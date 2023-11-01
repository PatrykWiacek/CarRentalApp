using CarRental.Logic.Models;
using CarRental.Logic.ServicesApi.IServiceApi;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RentalApiController : ControllerBase
{
    private readonly IRentalApiService _RentalApiService;

    public RentalApiController(IRentalApiService RentalApiService)
    {
        _RentalApiService = RentalApiService;
    }

    [HttpGet]
    public async Task<IEnumerable<RentalViewModel>> Get()
    {
        var getAcces = await _RentalApiService.GetAll();
        return getAcces;
    }

    [HttpGet("{id}")]
    public async Task<RentalViewModel> Get(int id)
    {
        var guest = await _RentalApiService.Get(id);
        return guest;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] RentalViewModel rental)
    {
        await _RentalApiService.Add(rental);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] RentalViewModel rental)
    {
        await _RentalApiService.Update(rental);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _RentalApiService.Delete(id);
        return Ok();
    }
}
