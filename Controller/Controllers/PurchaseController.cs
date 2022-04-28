using Microsoft.AspNetCore.Mvc;
using DTO;

namespace Controller.Controllers;

[ApiController]
[Route("[controller]")]
public class PurchaseController : ControllerBase
{
    public void getClientPurchase(int clientID)
    {
        
    }

    public void getStorePurchase(int storeID)
    {

    }

    public void makePurchase(PurchaseDTO purchase)
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
