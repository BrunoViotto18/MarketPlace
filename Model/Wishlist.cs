namespace Model;

using Interfaces;
using DTO;

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
}
 