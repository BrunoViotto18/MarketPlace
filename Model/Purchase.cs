namespace Model;

using Enums;
using Interfaces;

public class Purchase : IValidateDataObject<Purchase>, IDataController<Purchase>
{
    // Atributos
    private DateTime date_purchase;
    private String number_confirmation;
    private String number_nf;
    private int payment_type;
    private int purchase_status;
    public Double purchase_value;

    private Client client;
    List<Product> products;


	// GET & SET
	public DateTime getDataPurchase()
	{
		return date_purchase;
	}
	public void setDataPurchase(DateTime date_purchase)
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
	public void setPaymentType(PaymentEnum payment_type)
	{
		this.payment_type = (int)payment_type;
	}

	public int getPurchaseStatus()
	{
		return purchase_status;
	}
	public void setPurchaseStatus(PurchaseStatusEnum purchase_status)
	{
		this.purchase_status = (int)purchase_status;
	}

	public double getPurchaseValue()
	{
		return purchase_value;
	}
	public void setPurchaseValue(double purchase_value)
	{
		this.purchase_value = purchase_value;
	}
	
	public Client getClient()
	{
		return client;
	}
	public void setClient(Client client)
	{
		this.client = client;
	}

	public List<Product> getProducts()
	{
		return products;
	}
	public void setProducts(List<Product> products)
	{
		this.products = products;
	}


    // Métodos

	public void updateStatus()
    {
		throw new NotImplementedException();
    }

    public Boolean validateObject(Purchase obj){
        if(this.date_purchase == default)
            return false; 
        
        if(this.number_confirmation == null)
            return false;

        if(this.number_nf == null)
            return false;

        if(this.purchase_value == 0)
            return false;

        return true;
    }
}
