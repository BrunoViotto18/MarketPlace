namespace Model;
using Enums;
public class Purchase
{
    // Atributos
    private DateTime date_purchase;
    private int number_confirmation;
    private String number_nf;
    private String payment_type;
    private PurchaseStatusEnum purchase_status;
    public double purchase_value;
    // Depend�ncias
    private Client client;
    List<Product> products;

    // Construtor
    public Purchase(Client client, Product product)
    {
        this.client = client;
        // this.products = new List<Product>();
        // this.products.Add(product);
    }

    // GET & SET
    public DateTime getDatePurchase()
    {
        return date_purchase;
    }
    public void setDatePurchase(DateTime date_purchase)
    {
        this.date_purchase = date_purchase;
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

    // M�todos


}
