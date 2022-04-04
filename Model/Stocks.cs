namespace Model;

public class Stocks
{
    // Atributos
    private int quantity;
    private Product product;
    private Store store;

    // Construtor
    public Stocks(Store store, Product product)
    {
        this.store = store;
        this.product = product;
    }

    // GET & SET
    public Store GetStore()
    {
        return store;
    }
    public void setStore(Store store)
    {
        this.store = store;
    }

    public int GetQuantity()
    {
        return quantity;
    }
    public void SetQuantity(int quantity)
    {
        this.quantity = quantity;
    }

    public Product GetProduct()
    {
        return product;
    }
    public void setProduct(Product product)
    {
        this.product = product;
    }
}
