using Microsoft.AspNetCore.Mvc;
using DTO;
using Model;

namespace Controller.Controllers;

[ApiController]
[Route("[controller]")]
public class PurchaseController : ControllerBase
{
    [HttpGet]
    [Route("clientPurchase/{clientID}")]
    public List<PurchaseDTO> getClientPurchase(int clientID)
    {
        return Purchase.getClientPurchases(clientID);
    }
    
    [HttpGet]
    [Route("storePurchase/{storeID}")]
    public List<PurchaseDTO> getStorePurchase(int storeID)
    {
        return Purchase.getStorePurchases(storeID);
    }

    [HttpPost]
    [Route("make")]
    public object makePurchase(PurchaseDTO purchase)
    {
        Purchase purchaseModel = Purchase.convertDTOToModel(purchase);
        var id = purchaseModel.save();

        return new
        {
            id = id,
            dataCompra = purchase.data_purchase,
            valorCompra = purchase.purchase_value,
            tipoPagamento = purchase.payment_type,
            statusCompra = purchase.purchase_status,
            numeroConfirmacao = purchase.confirmation_number,
            numeroNF = purchase.number_nf,
            loja = purchase.store,
            cliente = purchase.client,
            produtos = purchase.productsDTO
        };
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
