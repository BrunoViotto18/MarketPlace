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
    [Route("addProduct/{stockId}")]
    public IActionResult addProductToWishList (int stockId)
    {
        int clientId = JWT.GetIdFromToken(Request.Headers["Authorization"].ToString());

        var wish = new WishList();

        return Ok(wish.save(clientId, stockId));
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

    [HttpGet]
    [Route("getWishlistState/{stockId}")]
    public IActionResult GetWishlistState(int stockId)
    {
        int clientId = -1;
        if (!String.IsNullOrWhiteSpace(Request.Headers["Authorization"].ToString()))
            clientId = JWT.GetIdFromToken(Request.Headers["Authorization"].ToString());

        var wish = WishList.getAllWishlists();
        wish.ForEach(w => w.includeClient());
        wish.ForEach(w => w.includeStocks());
        var b = wish.FirstOrDefault(w => w.getClient().getId() == clientId && w.getStocks().FirstOrDefault(s => s.getId() == stockId) != null);

        return Ok(b != null);
    }

    [HttpDelete]
    [Route("removeProduct/{stockId}")]
    public IActionResult removeProductToWishList(int stockId)
    {
        int clientId = JWT.GetIdFromToken(Request.Headers["Authorization"].ToString());

        var wish = WishList.getAllWishlists();
        wish.ForEach(w => w.includeClient());
        var wishlist = wish.FirstOrDefault(w => w.getClient().getId() == clientId);

        if (wishlist == null)
            return BadRequest();

        wishlist.includeStocks();
        wishlist.delete();

        return Ok(wishlist);
    }
}
