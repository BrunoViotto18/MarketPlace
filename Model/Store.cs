namespace Model;

using Interfaces;

public class Store : IValidateDataObject<Store>
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
    

    //Métodos

    // Adiciona uma nova compra
    public void addNewPurchase(Purchase purchase)
    {
        purchases.Add(purchase);
    }

    public Boolean validateObject()
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
}
