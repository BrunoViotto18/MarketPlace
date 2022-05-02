using Microsoft.AspNetCore.Mvc;
using DTO;

namespace Controller.Controllers;

[ApiController]
[Route("[controller]")]
public class StockController : ControllerBase
{
    [HttpPost]
    [Route("addProduct")]
    public void addProductToStock(object request)
    {

    }

    [HttpPut]
    [Route("update")]
    public void updateStock(object request)
    {
    
    }
}
