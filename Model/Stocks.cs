namespace Model;

using Interfaces;
using DTO;
using DAO;

public class Stocks : IValidateDataObject, IDataController<StocksDTO, Stocks>
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

    public Boolean validateObject()
    {
        if (this.quantity == 0)
            return false;

        if (this.product == null)
            return false;

        if (this.store == null)
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

    public int save(int storeId, int productId, int quantity, double unit_price)
    {
        int id;

        using (var context = new DAOContext())
        {
            var storeDao = context.Store.Where(s => s.id == storeId).Single();
            var productDao = context.Product.Where(p => p.id == productId).Single();

            DAO.Stocks stocks = new DAO.Stocks
            {
                quantity = quantity,
                unit_price = unit_price,
                product = productDao,
                store = storeDao
            };

            context.Stocks.Add(stocks);
            context.Entry(stocks.product).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            context.Entry(stocks.store).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            context.SaveChanges();

            id = stocks.id;
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

    public static Stocks convertDAOToModel(DAO.Stocks stocks)
    {
        return new Stocks
        {
            quantity = (int)stocks.quantity,
            unit_price = stocks.unit_price,
            product = Product.convertDAOToModel(stocks.product),
            store = Store.convertDAOToModel(stocks.store)
        };
    }

    public void delete()
    {

    }

    public void update()
    {

    }

    public StocksDTO findById()
    {

        return new StocksDTO();
    }

    public List<StocksDTO> getAll()
    {
        List<StocksDTO> stocks = new List<StocksDTO>();
        return stocks;
    }

}
