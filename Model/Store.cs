namespace Model;
using DTO;
using DAO;
using Interfaces;
using Microsoft.EntityFrameworkCore;

public class Store : IValidateDataObject, IDataController<StoreDTO, Store>
{
    // Atributos
    private int id;
    private String name;
    private String CNPJ;

    private Owner owner;
    private List<Purchase> purchases = new List<Purchase>();


    // Construtores
    public Store()
    {

    }

    public Store(int id, String name, String CNPJ)
    {
        this.id = id;
        this.name = name;
        this.CNPJ = CNPJ;
    }

    private Store(Owner owner)
    {
        this.owner = owner;
    }

    private Store(int id, Owner owner)
    {
        this.id = id;
        this.owner = owner;
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

        if (this.owner == null)
            return false;

        if (this.purchases == null)
            return false;

        return true;
    }

    /* Conversores */

    // Converte um objeto DTO para Model   // DETALHES A DISCUTIR//
    
    public static Store convertDTOToModelRegister(StoreRegisterDTO store)
    {
        return new Store
        {
            name = store.name,
            CNPJ = store.CNPJ
        };
    }

    public static Store convertDTOToModel(StoreDTO store)
    {
        if (store.purchases == null)
            store.purchases = new List<PurchaseDTO>();

        List<Purchase> purchases = new List<Purchase>();
        foreach (var purch in store.purchases)
            purchases.Add(Purchase.convertDTOToModel(purch));

        return new Store
        {
            name = store.name,
            CNPJ = store.CNPJ,
            owner = Owner.convertDTOToModel(store.owner),
            purchases = purchases
        };
    }

    // Converte um objeto Model para DTO
    public StoreDTO convertModelToDTO()
    {
        List<PurchaseDTO> purchases = new List<PurchaseDTO>();
        foreach (Purchase purch in this.purchases)
            purchases.Add(purch.convertModelToDTO());

        return new StoreDTO
        {
            name = this.name,
            CNPJ = this.CNPJ,
            owner = this.owner.convertModelToDTO(),
            purchases = purchases
        };
    }

    // Converte um objeto DAO para Model
    public static Store convertDAOToModel(DAO.Store store, bool purchaseStore=true)
    {
        List<Purchase> purchases = new List<Purchase>();
        if (purchaseStore)
        {
            using (var context = new DAOContext())
            {
                var purch = context.Purchase
                    .Include(p => p.client)
                        .ThenInclude(c => c.address)
                    .Include(p => p.product)
                    .Include(p => p.store)
                    .Where(p => p.store.id == store.id);

                foreach (var p in purch)
                    purchases.Add(Purchase.convertDAOToModel(p, false));
            }
        }

        return new Store
        {
            id = store.id,
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
            var ownerDao = context.Owner.FirstOrDefault(o => o.id == ownerId);

            Console.Write("entrou");

            if (ownerDao == null)
                return -2;

            var store = new DAO.Store
            {
                name = this.name,
                CNPJ = this.CNPJ,
                owner = ownerDao
            };

            if (context.Store.FirstOrDefault(s => s.CNPJ == store.CNPJ) != null)
                return -3;

            context.Store.Add(store);
            context.Entry(store.owner).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            context.SaveChanges();

            id = store.id;
        }

        return id;
    }


    public void delete()
    {
        using (var context = new DAOContext())
        {
            var store = context.Store.FirstOrDefault(s => s.CNPJ == this.CNPJ);

            if (store == null)
                return;

            context.Store.Remove(store);
            context.SaveChanges();
        }
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
            var store = context.Store
                .Include(s => s.owner)
                    .ThenInclude(o => o.address)
                .FirstOrDefault(s => s.CNPJ == CNPJ);

            return new StoreDTO
            {
                name = store.name,
                CNPJ = store.CNPJ,
                owner = Owner.convertDAOToModel(store.owner).convertModelToDTO(),
                purchases = new List<PurchaseDTO>()
            };
        }
    }


    public static Store? findById(int id)
    {
        using var context = new DAOContext();

        var storeDao = context.Store.FirstOrDefault(s => s.id == id);

        return new Store
        {
            name = storeDao.name,
            CNPJ = storeDao.CNPJ
        };
    }

    // Retorna todas as lojas
    public static List<Store> getAllStores()
    {
        List<Store> lojas = new List<Store>();
        using var context = new DAOContext();

        var stores = context.Store
            .Include(s => s.owner)
                .ThenInclude(o => o.address)
            .Include(s => s.owner);

        foreach (var store in stores)
            lojas.Add(Store.convertDAOToModel(store, false));

        return lojas;
    }
    public static List<Store> getAllOwnerStores(int id)
    {
        Console.WriteLine("oi");
        List<Store> lojas = new List<Store>();
        using var context = new DAOContext();

        var stores = context.Store
            .Include(s => s.owner)
                .ThenInclude(o => o.address).Where(s => s.owner.id == id);

        foreach (var store in stores)
            lojas.Add(Store.convertDAOToModel(store, false));

        
        return lojas;
    }

    // Retorna todas as lojas
    public List<StoreDTO> getAll()
    {
        List<StoreDTO> lojas = new List<StoreDTO>();
        return lojas;
    }

    public static int findId(string CNPJ)
    {
        using (var context = new DAOContext())
        {
            var store = context.Store.Where(s => s.CNPJ == CNPJ).Single();
            return store.id;
        }
    }


    // Trash

    public StoreDTO findById()
    {
        return new StoreDTO();
    }
}
