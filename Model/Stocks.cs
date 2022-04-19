namespace Model;

using Interfaces;
using DTO;
using DAO;

public class Stocks : IValidateDataObject<Stocks>, IDataController<StocksDTO, Stocks>
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


    // Métodos

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

    public static Stocks convertDTOToModel(StocksDTO stocks)
    {
        Stocks modelStocks = new Stocks();

        modelStocks.quantity = stocks.quantity;
        modelStocks.unit_price = stocks.unit_price;
        modelStocks.product = Product.convertDTOToModel(stocks.product);
        modelStocks.store = Store.convertDTOToModel(stocks.store);

        return modelStocks;
    }

    public int save()
    {
        int id = 0;

        using (var context = new DaoContext())
        {
            var product = new DAO.Product
            {
                name = this.product.getName(),
                bar_code = this.product.getBarCode()
            };

            DAO.Stocks stocks = new DAO.Stocks
            {
                quantity = this.quantity,
                unit_price = this.unit_price,
            };
        }

        return id;
    }

    public StocksDTO convertModelToDTO()
    {
        StocksDTO dtoStocks = new StocksDTO();

        dtoStocks.quantity = this.quantity;
        dtoStocks.unit_price = this.unit_price;
        dtoStocks.store = this.store.convertModelToDTO();
        dtoStocks.product = this.product.convertModelToDTO();

        return dtoStocks;
    }
}
