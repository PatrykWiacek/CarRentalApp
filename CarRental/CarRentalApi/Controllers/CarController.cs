using CarRental.Logic.Models;
using CarRental.Logic.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarRentalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;

    public CarController(ICarService carService)
    {
        _carService = carService;
    }

    [HttpGet]
    public async Task<IEnumerable<CarViewModel>> Get()
    {
        var getAcces = await _carService.GetAll();
        Response.Headers.Add("Access-Control-Allow-Origin", "https://localhost:7015");
        return getAcces;
    }

    [HttpGet("{id}")]
    public async Task<CarViewModel> Get(int id)
    {
        var guest = await _carService.Get(id);
        return guest;
    }

    [HttpPost]
    public IActionResult Add([FromBody] CarViewModel guest)
    {
        _carService.Create(guest);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] CarViewModel guest)
    {
        await _carService.Update(guest);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _carService.Delete(id);
        return Ok();
    }
}
