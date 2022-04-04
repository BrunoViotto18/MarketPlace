namespace Model;

using Interfaces;

public class Client : Person, IValidateDataObject<Client>
{
    // Atributos
    private Guid uuid = Guid.NewGuid();

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

    public Boolean validateObject(Client obj)
    {
        if (name == null)
            return false;

        if (date_of_birth == default)
            return false;

        if (document == null)
            return false;

        if (email == null)
            return false;

        if (phone == null)
            return false;

        if (login == null)
            return false;

        if (address == null)
            return false;

        return true;
    }
}