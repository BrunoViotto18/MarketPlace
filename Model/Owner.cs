namespace Model;

public class Owner : Person
{
    // Atributos
    private static Owner owner;

    // Construtor
    private Owner(Address address) : base(address)
    {

    }

    // Métodos

    // Retorna uma instância/objeto desta classe
    public static Owner getInstance(Address address)
    {
        if (owner == null)
        {
            owner = new Owner(address);
        }
        return owner;
    }
}
