namespace DTO;

public class StoreDTO
{
    public String name;
    public String CNPJ;

    public OwnerDTO owner;
    public List<PurchaseDTO> purchases;
}
