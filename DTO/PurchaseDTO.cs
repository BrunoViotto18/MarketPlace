namespace DTO;

public class PurchaseDTO
{
    public DateTime data_purchase;
    public Double purchase_value;
    public int payment_type;
    public int purchase_status;
    public String confirmation_number;
    public String number_nf;

    public StoreDTO store;
    public ClientDTO client;
    public List<ProductDTO> productsDTO = new List<ProductDTO>();
}
