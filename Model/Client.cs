namespace Model;

public class Client : Person
{
    // Atributos
    private static Client instance;
    private Guid uuid;
    // Construtor
    public Client(Address address) : base(address)
    {

    }

    // M�todos

    // Retorna uma inst�ncia/objeto desta classe
    public static Client getInstance(Address address)
    {
        if(instance == null)
        {
            instance = new Client(address);
        }
        return instance;
    }
}