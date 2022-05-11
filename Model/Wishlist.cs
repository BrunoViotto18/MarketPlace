namespace Model;

using Interfaces;
using DTO;
using DAO;

public class WishList : IValidateDataObject, IDataController<WishListDTO, WishList>
{
    // Atributos
    private Client client;
    private List<Product> products = new List<Product>(); 


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
        if (wishlist.products == null)
            wishlist.products = new List<ProductDTO>();

        List<Product> products = new List<Product>();
        foreach (ProductDTO prod in wishlist.products)
            products.Add(Product.convertDTOToModel(prod));

        return new WishList(Client.convertDTOToModel(wishlist.client))
        {
            products = products
        };
    }

    // Converte um objeto Model para DTO
    public WishListDTO convertModelToDTO()
    {
        List<ProductDTO> products = new List<ProductDTO>();
        foreach (Product prod in this.products)
            products.Add(prod.convertModelToDTO());

        return new WishListDTO
        {
            client = this.client.convertModelToDTO(),
            products = products
        };
    }

    // Converte um objeto DAO para Model
    public static WishList convertDAOToModel(DAO.WishList wishlist)
    {
        List<Product> products = new List<Product>();
        using (var context = new DAOContext())
        {
            var wishlists = context.WishList.Where(w => w.client == wishlist.client);
            foreach (var p in wishlists)
                products.Add(Product.convertDAOToModel(p.product));
        }

        return new WishList
        {
            client = Client.convertDAOToModel(wishlist.client),
            products = products
        };
    }


    /* Métodos SQL */


    // Salva o objeto atual no banco de dados
    public int save(String document, int productID)
    {
        int id;

        using (var context = new DAOContext())
        {
            var clientDao = context.Client.FirstOrDefault(c => c.document == document);
            var productDao = context.Product.FirstOrDefault(p => p.id == productID);

            if (clientDao == null || productDao == null)
            {
                Console.WriteLine("A");
                return -1;
            }

            var wishlist = new DAO.WishList
            {
                client = clientDao,
                product = productDao
            };

            if (context.WishList.FirstOrDefault(w => w.client == wishlist.client && w.product.bar_code == wishlist.product.bar_code) != null)
                return -1;

            context.WishList.Add(wishlist);
            context.Entry(wishlist.client).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            context.Entry(wishlist.product).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            context.SaveChanges();

            id = wishlist.id;
        }

        return id;
    }

    // Deleta o objeto atual no banco de dados
    public void delete()
    {
        using (var context = new DAOContext())
        {
            foreach (var prod in this.products)
            {
                var wishlist = context.WishList.FirstOrDefault(w => w.client.document == this.client.getDocument() && w.product.bar_code == prod.getBarCode());

                if (wishlist == null)
                    continue;

                context.WishList.Remove(wishlist);
                context.SaveChanges();
            }
        }
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
 