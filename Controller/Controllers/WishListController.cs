using Microsoft.AspNetCore.Mvc;
using DTO;
using Model;

namespace Controller.Controllers;

[ApiController]
[Route("[controller]")]
public class WishListController: ControllerBase
{
    [HttpPost]
    [Route("addProduct")]
    public object addProductToWishList ([FromBody] WishListDTO request)
    {
        WishList wishlist = WishList.convertDTOToModel(request);

        List<object> products = new List<object>();
        foreach (var prod in wishlist.getProducts())
        {
            wishlist.save(wishlist.getClient().getDocument(), prod.findId());
            products.Add(new
            {
                nome = prod.getName(),
                codigoDeBarras = prod.getBarCode()
            });
        }

        return new
        {
            cliente = request.client,
            produtos = products
        };
    }

    [HttpDelete]
    [Route("removeProduct")]
    public void removeProductToWishList([FromBody] WishListDTO request)
    {
        var wishlist = WishList.convertDTOToModel(request);
        wishlist.delete();
    }
}
