namespace Model;

public class Owner : Person
{
    private static Owner owner;
    public static Owner getInstance()
    {
        if (owner == null)
        {
            owner = new Owner();
        }
        return owner;
    }
}
