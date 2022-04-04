namespace Model;

using Enums;

public class Purchase
{
    // Atributos
    private DateTime date_purchase;
    private String number_confirmation;
    private String number_nf;
    private int payment_type;
    private int purchase_status;
    public double purchase_value;


    // Dependências
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

	public String getNumberConfirmation()
	{
		return number_confirmation;
	}
	public void setNumberConfirmation(String number_confirmation)
	{
		this.number_confirmation = number_confirmation;
	}

	public String getNumberNf()
	{
		return number_nf;
	}
	public void setNumberNf(String number_nf)
	{
		this.number_nf = number_nf;
	}

	public int getPaymentType()
	{
		return payment_type;
	}
	public void setPaymentType(int payment_type)
	{
		this.payment_type = payment_type;
	}

	public int getPurchaseStatus()
	{
		return purchase_status;
	}
	public void setPurchaseStatus(int purchase_status)
	{
		this.purchase_status = purchase_status;
	}

	public double getPurchaseValue()
	{
		return purchase_value;
	}
	public void setPurchaseValue(double purchase_value)
	{
		this.purchase_value = purchase_value;
	}

	public List<Product> getProducts()
    {
        return products;
    }


    // Métodos

	public void updateStatus()
    {
		throw new NotImplementedException();
    }
}
