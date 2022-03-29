namespace Model;

public class Owner : Person
{
    private static Owner owner;

    private Owner(Address address) : base(address)
    {

    }

    public static Owner getInstance(Address address)
    {
        if (owner == null)
        {
            owner = new Owner(address);
        }
        return owner;
    }
}
