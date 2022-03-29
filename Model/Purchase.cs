namespace Model;

public class Purchase
{
    private DateTime date_purchase;
    private String payment; 
    private int number_confirmation;
    private String number_nf;
    private Client client;
    List<Purchase> purchases;
}
