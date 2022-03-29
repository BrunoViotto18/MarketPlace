namespace Model;

public class Wishlist
{
    private Client client;
    List<Product> products; 

    public Wishlist(Client client)
    {
        this.products = new List<Product>();
        this.client = client;
    }

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

    public void addProductToWishList(Product product)
    {
        products.Add(product);
    }

    
}
 