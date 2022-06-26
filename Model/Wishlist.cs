namespace Model;

using Microsoft.EntityFrameworkCore;
using Interfaces;
using DTO;
using DAO;

public class WishList : IValidateDataObject, IDataController<WishListDTO, WishList>
{
    // Atributos
    private int id;
    private Client client;
    private List<Stocks> stocks = new List<Stocks>(); 


    // Construtor
    private WishList(Client client)
    {
        this.client = client;
    }

    public WishList()
    {

    }

    public WishList(List<Stocks> stocks)
    {
        this.stocks = stocks;
    }

    public WishList(int id)
    {
        this.id = id;
    }


    // GET & SET
    public int getId()
    {
        return id;
    }

    public Client getClient()
    {
        return client;
    }
    public void setClient(Client client)
    {
        this.client = client;
    }

    public List<Stocks> getStocks()
    {
        return stocks;
    }


    // Métodos

    // Adiciona um produto para a Wishlist
    public void addProductToWishList(Stocks stock)
    {
        stocks.Add(stock);
    }

    // Valida se o objeto tem todos os seus campos diferente de nulo
    public Boolean validateObject()
    {
        if (this.client == null)
            return false;

        if (this.stocks == null)
            return false;

        return true;
    }


    /* Conversores */


    // Converte um objeto DTO para Model
    public static WishList convertDTOToModel(WishListDTO wishlist)
    {
        if (wishlist.stocks == null)
            wishlist.stocks = new List<StocksDTO>();

        List<Stocks> stocks = new List<Stocks>();
        foreach (StocksDTO prod in wishlist.stocks)
            stocks.Add(Stocks.convertDTOToModel(prod));

        return new WishList(Client.convertDTOToModel(wishlist.client))
        {
            stocks = stocks
        };
    }

    // Converte um objeto Model para DTO
    public WishListDTO convertModelToDTO()
    {
        List<StocksDTO> products = new List<StocksDTO>();
        foreach (Stocks prod in this.stocks)
            products.Add(prod.convertModelToDTO());

        return new WishListDTO
        {
            client = this.client.convertModelToDTO(),
            stocks = products
        };
    }

    // Converte um objeto DAO para Model
    public static WishList convertDAOToModel(DAO.WishList wishlist)
    {
        List<Stocks> stocks = new List<Stocks>();
        using (var context = new DAOContext())
        {
            var wishlists = context.WishList
                .Include(w => w.client)
                    .ThenInclude(c => c.address)
                .Include(w => w.stock)
                    .ThenInclude(s => s.product)
                .Include(w => w.stock)
                    .ThenInclude(s => s.store)
                        .ThenInclude(s => s.owner)
                            .ThenInclude(o => o.address)
                .Where(w => w.client == wishlist.client);
            foreach (var wish in wishlists)
                stocks.Add(Stocks.convertDAOToModel(wish.stock));
        }

        return new WishList
        {
            id = wishlist.id,
            client = Client.convertDAOToModel(wishlist.client),
            stocks = stocks
        };
    }


    /* Métodos SQL */


    // Salva o objeto atual no banco de dados
    public int save(String document, int productID)
    {
        int id;

        using (var context = new DAOContext())
        {
            var clientDao = context.Client.FirstOrDefault(c => c.document == document);
            var stockDao = context.Stocks.FirstOrDefault(p => p.id == productID);

            if (clientDao == null || stockDao == null)
                return -1;

            var wishlist = new DAO.WishList
            {
                client = clientDao,
                stock = stockDao
            };

            if (context.WishList.FirstOrDefault(w => w.client == wishlist.client && w.stock.id == wishlist.stock.id) != null)
                return -1;

            context.WishList.Add(wishlist);
            context.Entry(wishlist.client).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            context.Entry(wishlist.stock).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            context.SaveChanges();

            id = wishlist.id;
        }

        return id;
    }

    // Deleta o objeto atual no banco de dados
    public void delete()
    {
        using (var context = new DAOContext())
        {
            foreach (var prod in this.stocks)
            {
                var wishlist = context.WishList.FirstOrDefault(w => w.client.document == this.client.getDocument() && w.stock.product.bar_code == prod.getProduct().getBarCode());

                if (wishlist == null)
                    continue;

                context.WishList.Remove(wishlist);
                context.SaveChanges();
            }
        }
    }


    public void update()
    {

    }

    public void includeClient()
    {
        using var context = new DAOContext();

        client = Client.convertDAOToModel(context.WishList.Include(w => w.client).ThenInclude(c => c.address).First(w => w.id == id).client);
    }

    public void includeStocks()
    {
        using var context = new DAOContext();

        foreach (var wish in context.WishList.Include(w => w.client).Include(w => w.stock).Where(w => w.client.id == client.getId()))
            stocks.Add(new Stocks(
                wish.stock.id,
                (int)wish.stock.quantity,
                wish.stock.unit_price
            ));
    }

    public static List<WishList> getAllWishlists()
    {
        using var context = new DAOContext();

        var wishlists = context.WishList.Include(w => w.client).ToList();

        var wishlist = new List<WishList>();
        foreach (var w in wishlists.GroupBy(w => w.client.id))
        {
            wishlist.Add(new WishList
            {
                id = w.First().id
            });
            if (wishlist.Last().id <= 0 || wishlist.Last().id == null)
                throw new Exception();
        }

        return wishlist;
    }


    /* Trash */

    public List<WishListDTO> getAll()
    {
        List<WishListDTO> list = new List<WishListDTO>();
        return list;
    }

    public WishListDTO findById()
    {
        return new WishListDTO();
    }
}
 