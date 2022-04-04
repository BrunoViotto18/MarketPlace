namespace Model;

public class Client : Person
{
    // Atributos
    private static Client instance;

    // Construtor
    public Client(Address address) : base(address)
    {

    }
    
    // GET & SET
    public Address getAddress()
    {
        return address;
    }
    public void setAddress(Address address)
    {
        this.address = address;
    }

    // Métodos

    // Retorna uma instância/objeto desta classe
    public static Client getInstance(Address address)
    {
        if(instance == null)
        {
            instance = new Client(address);
        }
        return instance;
    }
}