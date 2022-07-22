using Microsoft.AspNetCore.Mvc;
using DTO;
using Model;

namespace Controller.Controllers;

[ApiController]
[Route("[controller]")]
public class StockController : ControllerBase
{
    [HttpGet]
    [Route("all")]
    public IActionResult getAll()
    {
        var stocks = Stocks.getAllStocks();

        var allStocks = new List<StocksDTO>();
        foreach (var s in stocks)
            allStocks.Add(new StocksDTO
            {
                id = s.getId(),
                quantity = s.getQuantity(),
                unit_price = s.getUnitPrice()
            });

        return Ok(allStocks);
    }

    [HttpGet]
    [Route("allWithProducts")]
    public IActionResult getAllWithProducts()
    {
        var stocks = Stocks.getAllStocks();

        var allStocks = new List<StocksDTO>();
        foreach (var s in stocks)
        {
            s.includeProducts();
            allStocks.Add(new StocksDTO
            {
                id = s.getId(),
                quantity = s.getQuantity(),
                unit_price = s.getUnitPrice(),
                product = new ProductDTO
                {
                    id = s.getProduct().getId(),
                    name = s.getProduct().getName(),
                    bar_code = s.getProduct().getBarCode(),
                    image = s.getProduct().getImage(),
                    description = s.getProduct().getDescription()
                }
            });
        }

        return Ok(allStocks);
    }

    [HttpPost]
    [Route("addProduct")]
    public object addProductToStock([FromBody] StocksDTOII stocks)
    {
        var stockModel = new Stocks();    
        Stocks.save(stocks.storeId,stocks.productId,stocks.quantity,stocks.unit_price);           
        return stocks;
    }   

    [HttpPut]
    [Route("update")]
    public void updateStock(object request)
    {
    
    }

    [HttpDelete]
    [Route("delete")]
    public void deleteStock([FromBody] StocksDTO stocks)
    {
        var stockModel = Stocks.convertDTOToModel(stocks);
        stockModel.delete();  
       
    }
}
