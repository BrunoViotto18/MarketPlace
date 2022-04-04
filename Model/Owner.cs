namespace Model;

public class Owner : Person
{
    // Atributos
    private static Owner owner;

    // Construtor
    private Owner(Address address) : base(address)
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
    public static Owner getInstance(Address address)
    {
        if (owner == null)
        {
            owner = new Owner(address);
        }
        return owner;
    }
}
