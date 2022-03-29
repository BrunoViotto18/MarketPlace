namespace Model;

public class Client : Person
{
    private static Address address;
    private static Client instance;

    public static Client getInstance(Address address)
    {
        if(instance == null)
        {
            instance = new Client(address);
        }
        return instance;
    }
    

    private Client(Address address)
    {
        Client.address = address;
    }

}