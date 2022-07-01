using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using DTO;
using Model;

namespace Controller.Controllers;

[ApiController]
[Route("[controller]")]
public class WishListController: ControllerBase
{
    [HttpPost]
    [Authorize]
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
    [Authorize]
    [Route("getClientWishlist")]
    public IActionResult getClientWishlist()
    {
        var clientID = JWT.GetIdFromToken(Request.Headers["Authorization"].ToString());

        // Pega todas as wishlists
        var wishlists = WishList.getAllWishlists();
        wishlists.ForEach(w => w.includeClient());

        // Pega a wishlist do cliente
        var wish = wishlists.FirstOrDefault(w => w.getClient().getId() == clientID);
        if (wish == null)
            return Ok(new WishListDTO());

        // Monta a WishlistDTO
        wish.includeStocks();
        wish.getStocks().ForEach(s => s.includeProducts());
        var wishlist = new WishListDTO();
        wishlist.id = wish.getId();
        wishlist.stocks = new List<StocksDTO>();
        foreach (var s in wish.getStocks())
            wishlist.stocks.Add(new StocksDTO
            {
                id = s.getId(),
                product = s.getProduct().convertModelToDTO()
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
