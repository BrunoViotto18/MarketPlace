namespace DTO;

public class PurchaseDTO
{
    public int id { get; set; }
    public DateTime data_purchase { get; set; }
    public Double purchase_value { get; set; }
    public int payment_type { get; set; }
    public int purchase_status { get; set; }
    public String confirmation_number { get; set; }
    public String number_nf { get; set; }

    public StoreDTO store { get; set; }
    public ClientDTO client { get; set; }
    public List<ProductDTO> productsDTO { get; set; } = new List<ProductDTO>();
}
