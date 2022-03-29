namespace Model;

public class Store
{
    // Atributos
    private String nome;
    private String CNPJ;
    private Owner owner;
    private List<Purchase> purchases;

    // Construtor
    private Store(Owner owner)
    {
        this.owner = owner;
        // this.purchases = new List<Purchase>();
    }

    // GET & SET
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

    // Adiciona uma "Purchase"(Compra) para a loja
    public void addNewPurchase(Purchase purchase)
    {
        purchases.Add(purchase);
    }
}
