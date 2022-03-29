namespace Model;

public class Store
{
    private String nome;
    private String CNPJ;
    private Owner owner;
    List<Purchase> purchases;

    public void addNewPurchase(Purchase purchase)
    {
        purchases.Add(purchase);
    }
}
