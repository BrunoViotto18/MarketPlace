using Microsoft.AspNetCore.Mvc;
using System.Linq;
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
        foreach (var prod in wishlist.getStocks())
        {
            var id = wishlist.save(wishlist.getClient().getDocument(), prod.findId());
            ids.Add(id);
            products.Add(new
            {
                nome = prod.getProduct().getName(),
                codigoDeBarras = prod.getProduct().getBarCode()
            });
        }

        return new
        {
            id = ids,
            cliente = request.client,
            produtos = products
        };
    }

    [HttpGet]
    [Route("getClientWishlist/{clientID}")]
    public IActionResult getClientWishlist(int clientID)
    {
        // Pega todas as wishlists
        var wishlists = WishList.getAllWishlists();
        wishlists.ForEach(w => w.includeClient());

        // Pega a wishlist do cliente
        var wishlistClient = wishlists.Where(w => w.getClient().getId() == clientID);
        var wish = wishlistClient.FirstOrDefault();
        if (wish == null)
            return Ok(new WishListDTO());

        // Monta a WishlistDTO
        wishlistClient.First().includeStocks();
        var wishlist = new WishListDTO();
        wishlist.id = wish.getId();
        wishlist.stocks = new List<StocksDTO>();
        foreach (var s in wishlistClient.First().getStocks())
            wishlist.stocks.Add(new StocksDTO
            {
                id = s.getId()
            });

        return Ok(wishlist);
    }

    [HttpDelete]
    [Route("removeProduct")]
    public void removeProductToWishList([FromBody] WishListDTO request)
    {
        WishList.convertDTOToModel(request).delete();
    }
}
