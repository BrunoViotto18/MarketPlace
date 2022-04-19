namespace Model;

using Interfaces;
using DTO;
using DAO;

public class WishList : IValidateDataObject<WishList>, IDataController<WishListDTO, WishList>
{
    // Atributos
    private Client client;
    List<Product> products; 


    // Construtor
    public WishList(Client client)
    {
        this.products = new List<Product>();
        this.client = client;
    }


    // GET & SET
    public Client getClient()
    {
        return client;
    }
    public void setClient(Client client)
    {
        this.client = client;
    }

    public List<Product> getProducts()
    {
        return products;
    }


    // M�todos

    // Adiciona um produto para a Wishlist
    public void addProductToWishList(Product product)
    {
        products.Add(product);
    }

    public Boolean validateObject(WishList wishlist)
    {
        if (client == null)
            return false;

        if (products == null)
            return false;

        return true;
    }

    public static WishList convertDTOToModel(WishListDTO wishlist)
    {
        WishList modelWishlist = new WishList(Client.convertDTOToModel(wishlist.client));

        List<Product> products = new List<Product>();
        foreach (ProductDTO prod in wishlist.products)
            products.Add(Product.convertDTOToModel(prod));
        modelWishlist.products = products;

        return modelWishlist;
    }

    public int save(int clientId, int productId)
    {
        int id;

        using (var context = new DaoContext())
        {
            var clientDao = context.Client.Where(c => c.id == clientId).Single();
            var productDao = context.Product.Where(p => p.id == productId).Single();

            var wishlist = new DAO.WishList
            {
                client = clientDao,
                product = productDao
            };

            context.WishList.Add(wishlist);
            context.Entry(wishlist.client).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            context.Entry(wishlist.product).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            context.SaveChanges();

            id = wishlist.id;
        }

        return id;
    }

    public WishListDTO convertModelToDTO()
    {
        WishListDTO dtoWishlist = new WishListDTO();

        dtoWishlist.client = this.client.convertModelToDTO();
        List<ProductDTO> products = new List<ProductDTO>();
        foreach (Product prod in this.products)
            products.Add(prod.convertModelToDTO());
        dtoWishlist.products = products;

        return dtoWishlist;
    }
}
 