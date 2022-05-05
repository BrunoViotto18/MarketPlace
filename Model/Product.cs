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

    // Valida se o objeto tem todos os seus campos diferente de nulo
    public Boolean validateObject()
    {
        if (this.name == null)
            return false;

        if (this.bar_code == null)
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
            bar_code = product.bar_code
        };
    }

    // Converte um objeto Model para DTO
    public ProductDTO convertModelToDTO()
    {
        return new ProductDTO
        {
            name = this.name,
            bar_code = this.bar_code
        };
    }

    // Converte um objeto DAO para Model
    public static Product convertDAOToModel(DAO.Product product)
    {
        return new Product()
        {
            name = product.name,
            bar_code = product.bar_code
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
                bar_code = this.bar_code
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

    }


    public void update()
    {

    }

    // Retorna o ID do objeto atual
    public int findId()
    {
        using (var context = new DAOContext())
        {
            var product = context.Product.Where(p => p.bar_code == this.bar_code).Single();
            return product.id;
        }
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

    public static int FindId(string bar_code)
    {
        using (var context = new DAOContext())
        {
            var product = context.Product.Where(s => s.bar_code == bar_code).Single();
            return product.id;


        }
    }
}
