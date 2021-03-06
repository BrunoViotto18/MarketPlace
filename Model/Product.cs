namespace Model;
using DAO;
using Interfaces;
using DTO;
using Microsoft.EntityFrameworkCore;

public class Product : IValidateDataObject, IDataController<ProductDTO, Product>
{
    // Atributos
    private int id;
    private String name;
    private String bar_code;
    private String image;
    private String description;


    // Construtor
    public Product()
    {

    }

    public Product(int id)
    {
        this.id = id;
    }


    // GET & SET
    public int getId()
    {
        return id;
    }

    public String getName()
    {
        return name;
    }
    public void setName(String name)
    {
        this.name = name;
    }

    public String getBarCode()
    {
        return bar_code;
    }
    public void setBarCode(String bar_code)
    {
        this.bar_code = bar_code;
    }

    public String getImage()
    {
        return image;
    }
    public void setImage(String image)
    {
        this.image = image;
    }

    public String getDescription()
    {
        return description;
    }
    public void setDescription(String description)
    {
        this.description = description;
    }


    // Métodos

    // Valida se o objeto tem todos os seus campos diferente de nulo
    public Boolean validateObject()
    {
        if (this.name == null)
            return false;

        if (this.bar_code == null)
            return false;

        if (this.image == null)
            return false;

        if (this.description == null)
            return false;

        return true;
    }


    /* Conversores */

    // Converte um objeto DTO para Model
    public static Product convertDTOToModel(ProductDTO product)
    {
        return new Product
        {
            name = product.name,
            bar_code = product.bar_code,
            image = product.image,
            description = product.description
        };
    }

    // Converte um objeto Model para DTO
    public ProductDTO convertModelToDTO()
    {
        return new ProductDTO
        {
            name = this.name,
            bar_code = this.bar_code,
            image = this.image,
            description = this.description
        };
    }

    // Converte um objeto DAO para Model
    public static Product convertDAOToModel(DAO.Product product)
    {
        return new Product()
        {
            id = product.id,
            name = product.name,
            bar_code = product.bar_code,
            image = product.image,
            description = product.description
        };
    }


    /* Métodos SQL */

    // Salva o objeto atual no banco de dados
    public int save()
    {
        int id;

        using (var context = new DAOContext())
        {
            var product = new DAO.Product
            {
                name = this.name,
                bar_code = this.bar_code,
                image = this.image,
                description = this.description
            };

            if (context.Product.FirstOrDefault(p => p.bar_code == product.bar_code) != null)
                return -1;

            context.Product.Add(product);
            context.SaveChanges();

            id = product.id;
        }

        return id;
    }


    public void delete()
    {
        using (var context = new DAOContext())
        {
            var product = context.Product.FirstOrDefault(p => p.bar_code == this.bar_code);

            if (product == null)
                return;

            context.Product.Remove(product);
            context.SaveChanges();
        }
    }


    public void update()
    {

    }

    // Retorna o ID do objeto atual
    public int findId()
    {
        using var context = new DAOContext();

        var product = context.Product.FirstOrDefault(p => p.bar_code == this.bar_code);

        if (product == null)
            return -1;

        return product.id;
    }


    public ProductDTO findById()
    {
        return new ProductDTO();
    }

    public static Product? findById(int id)
    {
        using var context = new DAOContext();

        var productDao = context.Product.FirstOrDefault(p => p.id == id);
        return Product.convertDAOToModel(productDao);
    }


    public static List<object> getAllProducts()
    {
        List<object> produtos = new List<object>();

        using var context = new DAOContext();

        var stocks = context.Stocks
            .Include(s => s.product)
            .ToList();

        foreach (var stock in stocks)
        {
            produtos.Add(new
            {
                id = stock.product.id,
                name = stock.product.name,
                bar_code = stock.product.bar_code,
                image = stock.product.image,
                description = stock.product.description,
                price = stock.unit_price
            });
        }

        return produtos;
    }


    public List<ProductDTO> getAll()
    {
        using (var context = new DAOContext())
        {
            List<ProductDTO> allProducts = new List<ProductDTO>();
            var products = context.Product.ToList();

            foreach (var product in products)
                allProducts.Add(Product.convertDAOToModel(product).convertModelToDTO());

            return allProducts;
        }
    }

    public static int FindId(string bar_code)
    {
        using var context = new DAOContext();

        var product = context.Product.FirstOrDefault(s => s.bar_code == bar_code);

        if (product == null)
            return -1;

        return product.id;
    }
}
