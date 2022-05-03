namespace Model;
using DTO;
using DAO;
using Interfaces;

public class Store : IValidateDataObject, IDataController<StoreDTO, Store>
{
    // Atributos
    private String name;
    private String CNPJ;

    private Owner owner;
    private List<Purchase> purchases = new List<Purchase>();


    // Construtores
    private Store(Owner owner)
    {
        this.owner = owner;
    }

    public Store()
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

    public String getCNPJ()
    {
        return CNPJ;
    }
    public void setCNPJ(String CNPJ)
    {
        this.CNPJ = CNPJ;
    }

    public Owner getOwner()
    {
        return owner;
    }
    public void setOwner(Owner owner)
    {
        this.owner = owner;
    }

    public List<Purchase> getPurchases()
    {
        return purchases;
    }
    

    //Métodos

    // Adiciona uma nova compra ao objeto
    public void addNewPurchase(Purchase purchase)
    {
        purchases.Add(purchase);
    }

    // Valida se o objeto tem todos os seus campos diferente de nulo
    public Boolean validateObject()
    {
        if (this.name == null)
            return false;

        if (this.CNPJ == null)
            return false;

        /*if (this.owner == null)
            return false;

        if (this.purchases == null)
            return false;*/

        return true;
    }


    /* Conversores */

    // Converte um objeto DTO para Model
    public static Store convertDTOToModel(StoreDTO store)
    {
        List<Purchase> purchases = new List<Purchase>();
        foreach (var purch in store.purchases)
            purchases.Add(Purchase.convertDTOToModel(purch));

        Store modelstore = new Store
        {
            name = store.name,
            CNPJ = store.CNPJ,
            owner = Owner.convertDTOToModel(store.owner),
            purchases = purchases
        };

        return modelstore;
    }

    // Converte um objeto Model para DTO
    public StoreDTO convertModelToDTO()
    {
        StoreDTO dtoStore = new StoreDTO();

        dtoStore.name = this.name;
        dtoStore.CNPJ = this.CNPJ;
        dtoStore.owner = this.owner.convertModelToDTO();
        List<PurchaseDTO> purchases = new List<PurchaseDTO>();
        foreach (Purchase prod in this.purchases)
            purchases.Add(prod.convertModelToDTO());
        dtoStore.purchases = purchases;

        return dtoStore;
    }

    // Converte um objeto DAO para Model
    public static Store convertDAOToModel(DAO.Store store)
    {
        List<Purchase> purchases = new List<Purchase>();
        using (var context = new DAOContext())
        {
            var purch = context.Purchase.Where(p => p.store.id == store.id);
            foreach (var p in purch)
                purchases.Add(Purchase.convertDAOToModel(p));
        }

        return new Store
        {
            name = store.name,
            CNPJ = store.CNPJ,
            owner = Owner.convertDAOToModel(store.owner),
            purchases = purchases
        };
    }


    /* Métodos SQL */

    // Salva o objeto atual no banco de dados
    public int save(int ownerId)
    {
        int id;

        using (var context = new DAOContext())
        {
            var ownerDao = context.Owner.Where(o => o.id == ownerId).Single();

            var store = new DAO.Store
            {
                name = this.name,
                CNPJ = this.CNPJ,
                owner = ownerDao
            };

            context.Store.Add(store);
            context.Entry(store.owner).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            context.SaveChanges();

            id = store.id;
        }

        return id;
    }


    public void delete()
    {

    }


    public void update()
    {

    }


    public static Store find(string CNPJ)
    {
        using (var context = new DAO.DAOContext())
        {
            var store = context.Store.Where(s => s.CNPJ == CNPJ).Single();

            return Store.convertDAOToModel(store);
        }
    }

    // Retorna a loja pelo CNPJ
    public static StoreDTO findByCNPJ(String CNPJ)
    {
        using (var context = new DAOContext())
        {
            var store = context.Store.Where(s => s.CNPJ == CNPJ).Single();
            return Store.convertDAOToModel(store).convertModelToDTO();
        }
    }


    public StoreDTO findById()
    {

        return new StoreDTO();
    }

    // Retorna todas as lojas
    public static List<StoreDTO> getAllStores()
    {
        List<StoreDTO> lojas = new List<StoreDTO>();
        using (var context = new DAOContext())
        {
            var stores = context.Store.Where(p => true);
            foreach (var store in stores)
                lojas.Add(Store.convertDAOToModel(store).convertModelToDTO());
        }
        return lojas;
    }

    // Retorna todas as lojas
    public List<StoreDTO> getAll()
    {
        List<StoreDTO> lojas = new List<StoreDTO>();
        return lojas;
    }
}
