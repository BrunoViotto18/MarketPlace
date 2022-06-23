namespace Model;

using Interfaces;
using DTO;
using DAO;

public class Stocks : IValidateDataObject, IDataController<StocksDTO, Stocks>
{
    // Atributos
    private int id;
    private int quantity;
    private Double unit_price;

    private Product product;
    private Store store;


    // Construtor
    public Stocks()
    {

    }

    public Stocks(int id)
    {
        this.id = id;
    }

    public Stocks(int id, int quantity, Double unit_price)
    {
        this.id = id;
        this.quantity = quantity;
        this.unit_price = unit_price;
        this.product = product;
        this.store = store;
    }

    public Stocks(int id, int quantity, Double unit_price, Product product, Store store)
    {
        this.id = id;
        this.quantity = quantity;
        this.unit_price = unit_price;
        this.product = product;
        this.store = store;
    }


    // GET & SET
    public int getId()
    {
        return id;
    }

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

    // Valida se o objeto tem todos os seus campos diferente de nulo
    public Boolean validateObject()
    {
        if (this.quantity == 0)
            return false;

        if (this.unit_price == 0)
            return false;

        if (this.product == null)
            return false;

        if (this.store == null)
            return false;

        return true;
    }


    /* Conversores */

    // Converte um objeto DTO para Model
    public static Stocks convertDTOToModel(StocksDTO stocks)
    {
        return new Stocks
        {
            quantity = stocks.quantity,
            unit_price = stocks.unit_price,
            product = Product.convertDTOToModel(stocks.product),
            store = Store.convertDTOToModel(stocks.store)
        };
    }

    // Converte um objeto Model para DTO
    public StocksDTO convertModelToDTO()
    {
        return new StocksDTO
        {
            quantity = this.quantity,
            unit_price = this.unit_price,
            store = this.store.convertModelToDTO(),
            product = this.product.convertModelToDTO()
        };
    }

    // Converte um objeto DAO para Model
    public static Stocks convertDAOToModel(DAO.Stocks stocks)
    {
        return new Stocks
        {
            id = stocks.id,
            quantity = (int)stocks.quantity,
            unit_price = stocks.unit_price,
            product = Product.convertDAOToModel(stocks.product),
            store = Store.convertDAOToModel(stocks.store)
        };
    }


    /* Métodos SQL */

    // Salva o objeto atual no banco de dados
    public int save(int storeId, int productId, int quantity, double unit_price)
    {
        int id;

        using (var context = new DAOContext())
        {
            var storeDao = context.Store.FirstOrDefault(s => s.id == storeId);
            var productDao = context.Product.FirstOrDefault(p => p.id == productId);

            if (storeDao == null)
                return -1;

            if (productDao == null)
                return -2;

            DAO.Stocks stocks = new DAO.Stocks
            {
                quantity = quantity,
                unit_price = unit_price,
                product = productDao,
                store = storeDao
            };

            if (context.Stocks.FirstOrDefault(s => s.store == stocks.store && s.product == stocks.product) != null)
                return -1;

            context.Stocks.Add(stocks);
            context.Entry(stocks.product).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            context.Entry(stocks.store).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            context.SaveChanges();

            id = stocks.id;
        }

        return id;
    }

    // Deleta o objeto atual do banco de dados
    public void delete()
    {
        using (var context = new DAOContext())
        {
            var stock = context.Stocks.FirstOrDefault(s => s.store.CNPJ == this.store.getCNPJ() && s.product.bar_code == this.product.getBarCode());

            if (stock == null)
                return;

            context.Stocks.Remove(stock);
            context.SaveChanges();
        };
    }


    public void update()
    {

    }

    public static Stocks? findById(int stockId)
    {
        using var context = new DAOContext();

        var stock = context.Stocks.FirstOrDefault(s => s.id == stockId);

        if (stock == null)
            return null;

        return new Stocks
        {
            id = stockId,
            quantity = (int)stock.quantity,
            unit_price = stock.unit_price
        };
    }

    public StocksDTO findById()
    {
        return new StocksDTO();
    }


    public int findId()
    {
        using var context = new DAO.DAOContext();

        var stock = context.Stocks.FirstOrDefault(s => s.product.bar_code == product.getBarCode() && s.store.CNPJ == store.getCNPJ());

        if (stock == null)
            return -1;

        return stock.id;
    }


    public List<StocksDTO> getAll()
    {
        List<StocksDTO> stocks = new List<StocksDTO>();
        return stocks;
    }
}
