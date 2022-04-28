using Microsoft.AspNetCore.Mvc;
using DTO;
using Model;

namespace Controller.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressController : ControllerBase
{
    [HttpPost]
    [Route("register")]
    public object registerAddress([FromBody] AddressDTO address)
    {
        var addressModel = Address.convertDTOToModel(address);
        var id = addressModel.save();

        return new
        {
            id = id,
            rua = address.street,
            cidade = address.city,
            estado = address.state,
            pais = address.country,
            codigoPostal = address.postal_code
        };
    }

    [HttpDelete]
    public void removeAddress(AddressDTO address)
    {

    }

    [HttpPut]
    public void updateAddress(AddressDTO address)
    {

    }

    /*
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
    */
}
