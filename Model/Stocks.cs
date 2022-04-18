namespace Model;

using Interfaces;

public class Stocks : IValidateDataObject<Stocks>, IDataController<Stocks>
{
    // Atributos
    private int quantity;
    private Double unit_price;

    private Product product;
    private Store store;


    // Construtor
    public Stocks()
    {

    }


    // GET & SET
    public Store getStore()
    {
        return store;
    }
    public void setStore(Store store)
    {
        this.store = store;
    }

    public int getQuantity()
    {
        return quantity;
    }
    public void setQuantity(int quantity)
    {
        this.quantity = quantity;
    }

    public Double getUnitPrice()
    {
        return unit_price;
    }
    public void setUnitPrice(Double unit_price)
    {
        this.unit_price = unit_price;
    }

    public Product getProduct()
    {
        return product;
    }
    public void setProduct(Product product)
    {
        this.product = product;
    }


    // M�todos

    public Boolean validateObject(Stocks stocks)
    {
        if (quantity == 0)
            return false;

        if (product == null)
            return false;

        if (store == null)
            return false;

        return true;
    }
}
