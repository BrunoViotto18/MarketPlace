using Microsoft.AspNetCore.Mvc;
using DTO;

namespace Controller.Controllers;

[ApiController]
[Route("[controller]")]
public class PurchaseController : ControllerBase
{
    [HttpGet]
    [Route("clientPurchase/{clientID}")]
    public object getClientPurchase(int clientID)
    {
        return new
        {
            id = clientID
        };
    }
    
    [HttpGet]
    [Route("storePurchase/{storeID}")]
    public int getStorePurchase(int storeID)
    {
        return storeID;
    }

    [HttpPost]
    [Route("make")]
    public PurchaseDTO makePurchase(PurchaseDTO purchase)
    {
        return null;
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
