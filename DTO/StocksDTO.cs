namespace DTO;

public class StocksDTO
{
    public int id { get; set; }
    public int quantity { get; set; }
    public Double unit_price { get; set; }

    public StoreDTO store { get; set; }
    public ProductDTO product { get; set; }
}
