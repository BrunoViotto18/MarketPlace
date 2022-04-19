namespace Model;
using DTO;
using DAO;
using Interfaces;

public class Store : IValidateDataObject<Store>, IDataController<StoreDTO, Store>
{
    // Atributos
    private String name;
    private String CNPJ;

    private Owner owner;
    private List<Purchase> purchases;


    // Construtor
    public Store(Owner owner)
    {
        this.owner = owner;
        // this.purchases = new List<Purchase>();
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
    

    //M�todos

    // Adiciona uma nova compra
    public void addNewPurchase(Purchase purchase)
    {
        purchases.Add(purchase);
    }

    public Boolean validateObject(Store store)
    {
        if (name == null)
            return false;

        if (CNPJ == null)
            return false;

        if (owner == null)
            return false;

        if (purchases == null)
            return false;

        return true;
    }

    public static Store convertDTOToModel(StoreDTO store)
    {
        Store modelstore = new Store(Owner.convertDTOTOModel(store.owner));
        modelstore.CNPJ = store.CNPJ;
        modelstore.name = store.name;
        return modelstore;
    }

    public int save(int ownerID)
    {
        int id;

        using (var context = new DaoContext())
        {
            var ownerDao = context.Owner.Where(o => o.id == ownerID).Single();

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

    public void delete()
    {

    }

    public void update()
    {

    }

    public StoreDTO findById()
    {

        return new StoreDTO();
    }

    public List<StoreDTO> getAll()
    {
        List<StoreDTO> stores = new List<StoreDTO>(); 
        return stores;
    }

}
