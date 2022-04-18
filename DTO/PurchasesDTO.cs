namespace DTO;

public class PurchaseDTO
{
    public DateTime data_purchase;
    public Double purchase_value;
    public int payment_type;
    public int purchase_status;
    public String number_confirmation;
    public String number_nf;

    public List<ProductDTO> products;
}
