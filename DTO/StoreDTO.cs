namespace DTO;

public class StoreDTO
{
    public int id { get; set; }
    public string name { get; set; }
    public string CNPJ { get; set; }

    public OwnerDTO owner { get; set; }
    public List<PurchaseDTO> purchases { get; set; }
}
