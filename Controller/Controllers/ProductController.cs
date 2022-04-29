using Microsoft.AspNetCore.Mvc;
using DTO;
using Model;

namespace Controller.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    [HttpGet]
    [Route("all")]
    public void allProducts()
    {
        
    }

    [HttpPost]
    [Route("create")]
    public void createProduct(ProductDTO product)
    {
    
    }

    [HttpDelete]
    [Route("delete")]
    public void deleteProduct(ProductDTO product)
    {
    
    }

    [HttpPut]
    [Route("update")]
    public void updateProduct(ProductDTO product)
    {
    
    }
}
