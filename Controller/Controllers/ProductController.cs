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
    public IActionResult allProducts()
    {
        var response = Product.getAllProducts();
        var result = new ObjectResult(response);

        Response.Headers.Add("Access-Control-Allow-Origin", "*");

        return result;
    }

    [HttpPost]
    [Route("create")]
    public object createProduct([FromBody] ProductDTO product)
    {
        Product productModel = Product.convertDTOToModel(product);
        var id = productModel.save();

        return new
        {
            id = id,
            nome = product.name,
            codigoDeBarra = product.bar_code,
            imagem = product.image,
            descricao = product.description
        };
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
