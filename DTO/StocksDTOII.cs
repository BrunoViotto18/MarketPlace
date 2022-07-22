namespace DTO;

public class StocksDTOII
{
    public int id { get; set; }
    public int quantity { get; set; }
    public Double unit_price { get; set; }

    public int storeId { get; set; }
    public int productId { get; set; }
}
