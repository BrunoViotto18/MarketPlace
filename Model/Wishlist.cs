namespace Model;

public class Wishlist
{
    // Atributos
    private Client client;
    List<Product> products; 

    // Construtor
    public Wishlist(Client client)
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

    // Métodos

    // Adiciona um produto para a Wishlist
    public void addProductToWishList(Product product)
    {
        products.Add(product);
    }
}
 