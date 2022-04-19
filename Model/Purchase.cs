namespace Model;
using DAO;
using Enums;
using Interfaces;
using DTO;

public class Purchase : IValidateDataObject<Purchase>, IDataController<PurchaseDTO, Purchase>
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


	public Purchase()
    {

    }


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

	public static Purchase convertDTOToModel(PurchaseDTO purchase)
    {
		Purchase modelPurchase = new Purchase();

		modelPurchase.date_purchase = purchase.data_purchase;
		modelPurchase.number_confirmation = purchase.number_confirmation;
		modelPurchase.number_nf = purchase.number_nf;
		modelPurchase.payment_type = purchase.payment_type;
		modelPurchase.purchase_status = purchase.purchase_status;
		modelPurchase.purchase_value = purchase.purchase_value;
		modelPurchase.client = Client.convertDTOToModel(purchase.client);

		List<Product> products = new List<Product>();

		foreach (ProductDTO prod in purchase.products)
			products.Add(Product.convertDTOToModel(prod));

		modelPurchase.products = products;

		return modelPurchase;
    }

	public int save()
    {
		int id = 0;

		using (var context = new DaoContext())
        {

			var purchase = new DAO.Purchase
			{
				number_confirmation = this.number_confirmation,
				number_nf = this.number_nf,
				payment_type = this.payment_type,
				purchase_status = this.purchase_status,
				date_purchase = this.date_purchase
			};

			context.Purchase.Add(purchase);
			context.SaveChanges();

			id = purchase.id;
        }
		return id;
    }

	public PurchaseDTO convertModelToDTO()
	{
		PurchaseDTO dtoPurchase = new PurchaseDTO();

		dtoPurchase.data_purchase = this.date_purchase;
		dtoPurchase.purchase_value = this.purchase_value;
		dtoPurchase.payment_type = this.payment_type;
		dtoPurchase.purchase_status = this.purchase_status;
		dtoPurchase.number_confirmation = this.number_confirmation;
		dtoPurchase.number_nf = this.number_nf;
		dtoPurchase.client = this.client.convertModelToDTO();
		List<ProductDTO> products = new List<ProductDTO>();
		foreach (Product prod in this.products)
			products.Add(prod.convertModelToDTO());
		dtoPurchase.products = products;

		return dtoPurchase;
	}

	public void delete()
	{

	}

	public void update()
	{

	}

	public ClientDTO findById()
	{

		return new ClientDTO();
	}

	public List<ClientDTO> getAll()
	{
		List<ClientDTO> client = new List<ClientDTO>();
		return client;
	}

}
