namespace Model;

using Interfaces;
using DTO;
using DAO;

public class WishList : IValidateDataObject, IDataController<WishListDTO, WishList>
{
    // Atributos
    private Client client;
    List<Product> products = new List<Product>(); 


    // Construtor
    private WishList(Client client)
    {
        this.client = client;
    }

    public WishList()
    {

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

    // Valida se o objeto tem todos os seus campos diferente de nulo
    public Boolean validateObject()
    {
        if (this.client == null)
            return false;

        if (this.products == null)
            return false;

        return true;
    }


    /* Conversores */

    // Converte um objeto DTO para Model
    public static WishList convertDTOToModel(WishListDTO wishlist)
    {
        WishList modelWishlist = new WishList(Client.convertDTOToModel(wishlist.client));

        List<Product> products = new List<Product>();
        foreach (ProductDTO prod in wishlist.products)
            products.Add(Product.convertDTOToModel(prod));
        modelWishlist.products = products;

        return modelWishlist;
    }

    // Converte um objeto Model para DTO
    public WishListDTO convertModelToDTO()
    {
        WishListDTO dtoWishlist = new WishListDTO();

        dtoWishlist.client = this.client.convertModelToDTO();
        List<ProductDTO> products = new List<ProductDTO>();
        foreach (Product prod in this.products)
            products.Add(prod.convertModelToDTO());
        dtoWishlist.products = products;

        return dtoWishlist;
    }

    // Converte um objeto DAO para Model
    public static WishList convertDAOToModel(DAO.WishList wishlist)
    {
        List<Product> products = new List<Product>();
        using (var context = new DAOContext())
        {
            var prod = context.Product.Where(p => p.id == wishlist.product.id);
            foreach (var p in prod)
            {
                products.Add(Product.convertDAOToModel(p));
            }
        }

        return new WishList
        {
            client = Client.convertDAOToModel(wishlist.client),
            products = products
        };
    }


    /* Métodos SQL */

    // Salva o objeto atual no banco de dados
    public int save(string document, int productId)
    {
        int id;

        using (var context = new DAOContext())
        {
            var clientDao = context.Client.Where(c => c.document == document).Single();
            var productDao = context.Product.Where(p => p.id == productId).Single();

            var wishlist = new DAO.WishList
            {
                client = clientDao,
                product = productDao
            };

            context.WishList.Add(wishlist);
            context.Entry(wishlist.client).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            context.Entry(wishlist.product).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            context.SaveChanges();

            id = wishlist.id;
        }

        return id;
    }

    public void delete()
    {

    }

    public void update()
    {

    }

    public WishListDTO findById()
    {

        return new WishListDTO();
    }

    public List<WishListDTO> getAll()
    {
        List<WishListDTO> wishlist = new List<WishListDTO>();
        return wishlist;
    }
}
 