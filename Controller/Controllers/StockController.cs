using Microsoft.AspNetCore.Mvc;
using DTO;
using Model;

namespace Controller.Controllers;

[ApiController]
[Route("[controller]")]
public class StockController : ControllerBase
{
    [HttpPost]
    [Route("addProduct")]
    public object addProductToStock([FromBody] StocksDTO stocks)
    {
        var stockModel = Stocks.convertDTOToModel(stocks);
        var storeId = Store.findId(stockModel.getStore().getCNPJ());
        var productid = Product.FindId(stockModel.getProduct().getBarCode());
        var id = stockModel.save(storeId, productid, stockModel.getQuantity(), stockModel.getUnitPrice());
        return new
        {
            id = id,
            quantity = stocks.quantity,   
            unit_price = stocks.unit_price, 
            product = stocks.product,
            store = stocks.store
        };
                
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
