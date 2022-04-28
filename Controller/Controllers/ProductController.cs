using Microsoft.AspNetCore.Mvc;
using DTO;

namespace Controller.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    public void allProducts() { }

    public void createProduct(ProductDTO product) { }

    public void deleteProduct(ProductDTO product) { }

    public void updateProduct(ProductDTO product) { }

    

}
