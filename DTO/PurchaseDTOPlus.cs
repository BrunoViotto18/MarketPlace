namespace DTO;

public class PurchaseDTOPlus
{
    public int id { get; set; }
    public DateTime data_purchase { get; set; }
    public Double purchase_value { get; set; }
    public int payment_type { get; set; }
    public int purchase_status { get; set; }
    public String confirmation_number { get; set; }
    public String number_nf { get; set; }

    public int storeId { get; set; }
    public int clientId { get; set; }
    public ProductDTO product { get; set; }
}
