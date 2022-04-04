namespace Model;

public class Store
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


    public void addNewPurchase(Purchase purchase)
    {
        purchases.Add(purchase);
    }
}
