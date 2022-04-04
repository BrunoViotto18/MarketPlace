namespace Model;

public class Client : Person
{
    // Atributos
    private static Client instance;

    // Construtor
    public Client(Address address) : base(address)
    {

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