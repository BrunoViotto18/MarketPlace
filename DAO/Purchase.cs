namespace DAO;

public class Purchase
{
    public int id;
    public string number_confirmation;
    public string number_nf;
    public int payment_type;
    public string purchase_status;
    public DateTime data_purchase;

    public Client client;
    public Product product;
    public Store store;
}
