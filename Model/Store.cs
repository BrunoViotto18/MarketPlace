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
    private List<Purchase> purchases;


    // Construtor
    private Store(Owner owner)
    {
        this.owner = owner;
    }

    public Store() { }
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

    // Adiciona uma nova compra
    public void addNewPurchase(Purchase purchase)
    {
        purchases.Add(purchase);
    }

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

    public static Store convertDTOToModel(StoreDTO store)
    {
        // Store modelstore = new Store(Owner.convertDTOToModel(store.owner));
        Store modelstore = new Store();
        modelstore.CNPJ = store.CNPJ;
        modelstore.name = store.name;
        return modelstore;
    }

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

    public StoreDTO find(string CNPJ)
    {
        using (var context = new DAO.DAOContext())
        {
            var store = context.Store.Where(s => s.CNPJ == CNPJ).Single();

            var owner = new Owner
            {

            }

            return new StoreDTO
            {
                name = store.name,
                CNPJ = store.CNPJ,
                owner = store.owner
            };
        }
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
