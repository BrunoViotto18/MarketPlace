namespace DTO;

public class OwnerDTO
{
    public int id { get; set; }
    public String name { get; set; }
    public DateTime date_of_birth { get; set; }
    public String document { get; set; }
    public String email { get; set; }
    public String phone { get; set; }
    public String login { get; set; }
    public String passwd { get; set; }
    public AddressDTO address { get; set; }
}
