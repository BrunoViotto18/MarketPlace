namespace Model;
using DAO;
using Interfaces;
using DTO;

public class Product : IValidateDataObject, IDataController<ProductDTO, Product>
{
    // Atributos
    private String name;
    private String bar_code;


    // Construtor
    public Product()
    {

    }


    // GET & SET
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


    // Métodos

    public Boolean validateObject()
    {
        if (this.name == null)
            return false;

        if (this.bar_code == null)
            return false;

        return true;
    }

    public static Product convertDTOToModel(ProductDTO product)
    {
        Product modelProduct = new Product
        {
            name = product.name,
            bar_code = product.bar_code
        };

        return modelProduct;
    }

    public int save()
    {
        int id;

        using (var context = new DAOContext())
        {
            var product = new DAO.Product
            {
                name = this.name,
                bar_code = this.bar_code
            };

            context.Product.Add(product);
            context.SaveChanges();

            id = product.id;
        }

        return id;
    }

    public ProductDTO convertModelToDTO()
    {
        ProductDTO dtoProduct = new ProductDTO();

        dtoProduct.name = this.name;
        dtoProduct.bar_code = this.bar_code;

        return dtoProduct;
    }

    public static Product convertDAOToModel(DAO.Product product)
    {
        return new Product()
        {
            name = this.name,
            bar_code = product.bar_code
        };
    }

    public void delete()
    {

    }

    public void update()
    {

    }

    public ProductDTO findById()
    {

        return new ProductDTO();
    }

    public List<ProductDTO> getAll()
    {
        List<ProductDTO> prod = new List<ProductDTO>();
        return prod;
    }
}
