namespace Model;
using DAO;
using Enums;
using Interfaces;
using DTO;
using System.Globalization;

public class Purchase : IValidateDataObject, IDataController<PurchaseDTO, Purchase>
{
    // Atributos
    private DateTime date_purchase;
    private String number_confirmation;
    private String number_nf;
    private int payment_type;
    private int purchase_status;
    public Double purchase_value;

    private Client client;
	private Store store;
    List<Product> products;


	// Construtor
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

	public Store getStore()
	{
		return store;
	}
	public void setStore(Store store)
	{
		this.store = store;
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

	// Valida se o objeto tem todos os seus campos diferente de nulo
	public Boolean validateObject(){
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


	/* Conversores */

	// Converte um objeto DTO para Model
	public static Purchase convertDTOToModel(PurchaseDTO purchase)
    {
		Purchase modelPurchase = new Purchase
		{
			date_purchase = purchase.data_purchase,
			number_confirmation = purchase.confirmation_number,
			number_nf = purchase.number_nf,
			payment_type = purchase.payment_type,
			purchase_status = purchase.purchase_status,
			purchase_value = purchase.purchase_value,
			client = Client.convertDTOToModel(purchase.client),
			store = Store.convertDTOToModel(purchase.store)
		};

		List<Product> products = new List<Product>();
		foreach (ProductDTO prod in purchase.productsDTO)
			products.Add(Product.convertDTOToModel(prod));

		modelPurchase.products = products;

		return modelPurchase;
	}

	// Converte um objeto Model para DTO
	public PurchaseDTO convertModelToDTO()
	{
		PurchaseDTO dtoPurchase = new PurchaseDTO();

		dtoPurchase.data_purchase = this.date_purchase;
		dtoPurchase.purchase_value = this.purchase_value;
		dtoPurchase.payment_type = this.payment_type;
		dtoPurchase.purchase_status = this.purchase_status;
		dtoPurchase.confirmation_number = this.number_confirmation;
		dtoPurchase.number_nf = this.number_nf;
		dtoPurchase.store = this.store.convertModelToDTO();
		dtoPurchase.client = this.client.convertModelToDTO();
		List<ProductDTO> products = new List<ProductDTO>();
		foreach (Product prod in this.products)
			products.Add(prod.convertModelToDTO());
		dtoPurchase.productsDTO = products;

		return dtoPurchase;
	}

	// Converte um objeto DAO para Model
	public static Purchase convertDAOToModel(DAO.Purchase purchase)
    {
		List<Product> products = new List<Product>();
		using (var context = new DAOContext())
        {
			var purch = context.Purchase.Where(p => p.number_nf == purchase.number_nf ); 
			foreach (var p in purch)
            {
				products.Add(Product.convertDAOToModel(p.product));
            }
        }
		
		return new Purchase(){
		date_purchase = purchase.date_purchase,
		number_confirmation =purchase.number_confirmation,
		number_nf = purchase.number_nf,
		purchase_status = purchase.purchase_status,
		purchase_value = purchase.purchase_value,
		client = Client.convertDAOToModel(purchase.client),
		store = Store.convertDAOToModel(purchase.store),
		products = products
		};
    }


	/* Métodos SQL */

	// Salva o objeto atual no banco de dados
	public int save()
    {
		int id;

		using (var context = new DAOContext())
		{
			//var clientDao = context.Client.FirstOrDefault(c => c.id == 1);
			//var storeDao = context.Store.Where(s => s.CNPJ == this.store.getCNPJ()).Single();
			var clientDao = context.Client.Where(c => c.document == this.client.getDocument()).Single();
			var storeDao = context.Store.Where(s => s.CNPJ == this.store.getCNPJ()).Single();

			if (this.products.Count() == 0)
				return -1;

			var productDao = context.Product.Where(p => p.bar_code == this.products.First().getBarCode()).Single();

			DAO.Purchase purchase = new DAO.Purchase
			{
				number_confirmation = this.number_confirmation,
				number_nf = this.number_nf,
				payment_type = this.payment_type,
				purchase_status = this.purchase_status,
				date_purchase = this.date_purchase,
				purchase_value = this.purchase_value,
				client = clientDao,
				store = storeDao,
				product = productDao
			};

            context.Purchase.Add(purchase);
			
			context.Entry(purchase.client).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
			context.Entry(purchase.store).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
			context.Entry(purchase.product).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
			context.SaveChanges();

			this.products.RemoveAt(0);
			this.save();

			id = purchase.id;
		}

		return id;
    }


	public void delete()
	{

	}


	public void update()
	{

	}


	public PurchaseDTO findById()
	{
		return new PurchaseDTO();
	}

	// Retorna todas as compras de um cliente
	public static List<PurchaseDTO> getClientPurchases(int clientID)
    {
		List<PurchaseDTO> clientPurchases = new List<PurchaseDTO>();
		using (var context = new DAOContext())
        {
			var purchases = context.Purchase.Where(p => p.client.id == clientID);
			foreach (var purch in purchases)
				clientPurchases.Add(Purchase.convertDAOToModel(purch).convertModelToDTO());
        }
		return clientPurchases;
	}

	// Retorna todas as compras de uma loja
	public static List<PurchaseDTO> getStorePurchases(int storeID)
	{
		List<PurchaseDTO> clientPurchases = new List<PurchaseDTO>();
		using (var context = new DAOContext())
		{
			var purchases = context.Purchase.Where(p => p.store.id == storeID);
			foreach (var purch in purchases)
				clientPurchases.Add(Purchase.convertDAOToModel(purch).convertModelToDTO());
		}
		return clientPurchases;
	}


	public List<PurchaseDTO> getAll()
	{
		List<PurchaseDTO> purchase= new List<PurchaseDTO>();
		return purchase;
	}
}
