namespace Model;

public class Store
{
    private String nome;
    private String CNPJ;
    private Owner owner;
    private List<Purchase> purchases;

    private Store()
    {

    }

    public String getNome()
    {
        return nome;
    }
    public void setNome(String nome)
    {
        this.nome = nome;
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

    // Métodos

    public void addNewPurchase(Purchase purchase)
    {
        purchases.Add(purchase);
    }
}
