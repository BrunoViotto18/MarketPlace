namespace Model;

public class Person
{
    protected Person(Address address)
    {
        this.address = address;
    }

    protected String name;
    protected int age;
    protected String document;
    protected String email;
    protected String phone;
    protected String login;
    protected Address address;
}
