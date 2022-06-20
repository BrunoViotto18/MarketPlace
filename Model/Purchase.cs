namespace Model;
using DAO;
using Enums;
using Interfaces;
using DTO;
using Microsoft.EntityFrameworkCore;

public class Purchase : IValidateDataObject, IDataController<PurchaseDTO, Purchase>
{
	// Atributos
	private int id;
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

	public Purchase(int id)
	{
		this.id = id;
	}


	// GET & SET
	public int getId()
	{
		return id;
	}

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

	// Atualiza o status da compra
	public void updateStatus()
    {
		throw new NotImplementedException();
    }

    public Boolean validateObject(){
        if(this.date_purchase == default)
            return false; 
        
        if(this.number_confirmation == null)
            return false;

        if(this.number_nf == null)
            return false;

        if(this.purchase_value == 0)
            return false;

		if (this.client == null)
			return false;

		if (this.store == null)
			return false;

		if (this.products == null)
			return false;

        return true;
    }

	public static Purchase convertDTOToModel(PurchaseDTO purchase)
	{
		if (purchase.productsDTO == null)
			purchase.productsDTO = new List<ProductDTO>();

		List<Product> products = new List<Product>();
		foreach (ProductDTO prod in purchase.productsDTO)
			products.Add(Product.convertDTOToModel(prod));

		return new Purchase
		{
			date_purchase = purchase.data_purchase,
			number_confirmation = purchase.confirmation_number,
			number_nf = purchase.number_nf,
			payment_type = purchase.payment_type,
			purchase_status = purchase.purchase_status,
			purchase_value = purchase.purchase_value,
			client = Client.convertDTOToModel(purchase.client),
			store = Store.convertDTOToModel(purchase.store),
			products = products
		};
    }

	// Converte um objeto Model para DTO
	public PurchaseDTO convertModelToDTO()
	{
		List<ProductDTO> products = new List<ProductDTO>();
		foreach (Product prod in this.products)
			products.Add(prod.convertModelToDTO());

		if (this.store == null)
			this.store = new Store();

		return new PurchaseDTO
		{
			data_purchase = this.date_purchase,
			purchase_value = this.purchase_value,
			payment_type = this.payment_type,
			purchase_status = this.purchase_status,
			confirmation_number = this.number_confirmation,
			number_nf = this.number_nf,
			store = this.store.convertModelToDTO(),
			client = this.client.convertModelToDTO(),
			productsDTO = products
		};
	}


	public static Purchase convertDAOToModel(DAO.Purchase purchase, bool storePurchase=true)
    {
		List<Product> products = new List<Product>();
		using (var context = new DAOContext())
        {
			var purch = context.Purchase.Where(p => p.number_nf == purchase.number_nf && p.client == purchase.client)
				.Include(p => p.product);
			foreach (var p in purch)
				products.Add(Product.convertDAOToModel(p.product));
        }

		Store store = null;
		if (storePurchase)
			store = Store.convertDAOToModel(purchase.store, false);

		return new Purchase
		{
			id = purchase.id,
			date_purchase = purchase.date_purchase,
			number_confirmation =purchase.number_confirmation,
			number_nf = purchase.number_nf,
			payment_type = purchase.payment_type,
			purchase_status = purchase.purchase_status,
			purchase_value = purchase.purchase_value,
			client = Client.convertDAOToModel(purchase.client),
			store = store,
			products = products
		};
    }

	public int save()
    {
		int id = 0;

		using (var context = new DAOContext())
		{
			var clientDao = context.Client.FirstOrDefault(c => c.document == this.client.getDocument());
			var storeDao = context.Store.FirstOrDefault(s => s.CNPJ == this.store.getCNPJ());
			var nf = context.Purchase.FirstOrDefault(p => p.number_nf == this.number_nf);

			if (clientDao == null || storeDao == null || nf != null)
				return -1;

			foreach (var prod in products)
			{
				var productDao = context.Product.FirstOrDefault(p => p.bar_code == this.products.First().getBarCode());
				if (productDao == null)
					return -1;

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

				if (context.Purchase.FirstOrDefault(p => p.number_nf == purchase.number_nf && p.product.bar_code == purchase.product.bar_code) != null)
					continue;

				context.Purchase.Add(purchase);
				context.Entry(purchase.client).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
				context.Entry(purchase.store).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
				context.Entry(purchase.product).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
				context.SaveChanges();

				id = purchase.id;
			}
		}

		return id;
	}

	public void delete()
	{
		using (var context = new DAO.DAOContext())
        {
			foreach (var prod in this.products)
			{
				var purchase = context.Purchase.FirstOrDefault(p => p.number_nf == this.number_nf && p.product.bar_code == prod.getBarCode());

				if (purchase == null)
					continue;

				context.Purchase.Remove(purchase);
				context.SaveChanges();
			}
        }
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
		using var context = new DAOContext();

		var purchases = context.Purchase
			.Include(p => p.client)
				.ThenInclude(c => c.address)
			.Include(p => p.store)
				.ThenInclude(s => s.owner)
					.ThenInclude(o => o.address)
			.Include(p => p.product)
			.Where(p => p.client.id == clientID)
			.ToList()
			.GroupBy(p => p.number_nf);

		foreach (var purch in purchases)
        {
			if (purch == null)
				continue;

			List<ProductDTO> products = new List<ProductDTO>();
			foreach (var p in purch)
				products.Add(Product.convertDAOToModel(p.product).convertModelToDTO());

			var compra = purch.FirstOrDefault();
			if (compra == null)
				continue;

			PurchaseDTO purchase = Purchase.convertDAOToModel(compra).convertModelToDTO();
			purchase.productsDTO = products;

			clientPurchases.Add(purchase);
        }

		return clientPurchases;
	}

	// Retorna todas as compras de uma loja
	public static List<PurchaseDTO> getStorePurchases(int storeID)
	{
		List<PurchaseDTO> clientPurchases = new List<PurchaseDTO>();
		using var context = new DAOContext();

		var purchases = context.Purchase
			.Include(p => p.client)
				.ThenInclude(c => c.address)
			.Include(p => p.store)
				.ThenInclude(s => s.owner)
					.ThenInclude(o => o.address)
			.Include(p => p.product)
			.Where(p => p.store.id == storeID)
			.ToList()
			.GroupBy(p => p.number_nf);

		foreach (var purch in purchases)
		{
			if (purch == null)
				continue;

			List<ProductDTO> products = new List<ProductDTO>();
			foreach (var p in purch)
				products.Add(Product.convertDAOToModel(p.product).convertModelToDTO());

			var compra = purch.FirstOrDefault();
			if (compra == null)
				continue;

			PurchaseDTO purchase = Purchase.convertDAOToModel(compra).convertModelToDTO();
			purchase.productsDTO = products;

			clientPurchases.Add(purchase);
		}

		return clientPurchases;
	}


	public List<PurchaseDTO> getAll()
	{
		List<PurchaseDTO> purchase= new List<PurchaseDTO>();
		return purchase;
	}
}
