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
        List<int> ids = new List<int>();
        foreach (var prod in wishlist.getProducts())
        {
            var id = wishlist.save(wishlist.getClient().getDocument(), prod.findId());
            ids.Add(id);
            products.Add(new
            {
                nome = prod.getName(),
                codigoDeBarras = prod.getBarCode()
            });
        }

        return new
        {
            id = ids,
            cliente = request.client,
            produtos = products
        };
    }

    [HttpDelete]
    [Route("removeProduct")]
    public void removeProductToWishList([FromBody] WishListDTO request)
    {
        WishList.convertDTOToModel(request).delete();
    }
}
