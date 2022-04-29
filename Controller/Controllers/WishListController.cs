using Microsoft.AspNetCore.Mvc;
using DTO;

namespace Controller.Controllers;

[ApiController]
[Route("[controller]")]
public class WishListController: ControllerBase
{
    [HttpPost]
    [Route("addProduct")]
    public void addProductToWishList (object request)
    {
    
    }

    [HttpDelete]
    [Route("removeProduct")]
    public void removeProductToWishList(object request)
    {
    
    }
}
