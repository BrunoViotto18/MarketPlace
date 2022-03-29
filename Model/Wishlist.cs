namespace Model;

public class Wishlist
{
    private Client client;
    List<Product> products; 

    public void addProductToWIshList(Product product)
    {
        products.Add(product);
    }
    public List<Product> getList()
    {
        return products;
    }

    
}
 