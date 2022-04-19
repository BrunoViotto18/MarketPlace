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

    public int save()
    {
        var id = 0;
        using (var context = new DaoContext())
        {
            var wishlist = new DAO.WishList
            {

            };

            context.WishList.Add(wishlist);
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

    public void delete()
    {

    }

    public void update()
    {

    }

    public WishListDTO findById()
    {

        return new WishListDTO();
    }

    public List<WishListDTO> getAll()
    {
        List<WishListDTO> wishlist = new List<WishListDTO>();
        return wishlist;
    }
}
 