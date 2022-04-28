namespace DAO;

public class Purchase
{
    public int id;
    public string number_confirmation;
    public string number_nf;
    public int payment_type;
    public int purchase_status;
    public DateTime date_purchase;
    public Double purchase_value;

    public Client client;
    public Product product;
    public Store store;
}
