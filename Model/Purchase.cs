namespace Model;

public class Purchase
{
    private DateTime date_purchase;
    private String payment; 
    private int number_confirmation;
    private String number_nf;
    private Client client;
    List<Product> products;

    public Purchase(Client client, Product product)
    {
        this.client = client;
        this.products = new List<Product>();
        this.products.Add(product);
    }

    public DateTime getDatePurchase()
    {
        return date_purchase;
    }
    public void setDatePurchase(DateTime date_purchase)
    {
        this.date_purchase = date_purchase;
    }

    public String getPayment()
    {
        return payment;
    }
    public void setPayment(String payment)
    {
        this.payment = payment;
    }

    public int getNumberConfirmation()
    {
        return number_confirmation;
    }
    public void setNumberConfirmation(int number_confirmation)
    {
        this.number_confirmation = number_confirmation;
    }

    public String getNumberNF()
    {
        return number_nf;
    }
    public void setNumberNF(String number_nf)
    {
        this.number_nf = number_nf;
    }

    public List<Product> getProducts()
    {
        return products;
    }
}
