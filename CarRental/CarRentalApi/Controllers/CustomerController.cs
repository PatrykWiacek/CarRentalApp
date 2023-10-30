using CarRental.Logic.Models;
using CarRental.Logic.ServicesApi.IServiceApi;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerApiService _customerService;

    public CustomerController(ICustomerApiService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<IEnumerable<CustomerViewModel>> Get()
    {
        var getAcces = await _customerService.GetAll();
        return getAcces;
    }

    [HttpGet("{id}")]
    public async Task<CustomerViewModel> Get(int id)
    {
        var guest = await _customerService.Get(id);
        return guest;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CustomerViewModel guest)
    {
        await _customerService.Add(guest);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] CustomerViewModel guest)
    {
        await _customerService.Update(guest);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _customerService.Delete(id);
        return Ok();
    }
}
