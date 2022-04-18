namespace Model;

public class Person
{
    // Atributos
    protected String name;
    protected DateTime date_of_birth;
    protected String document;
    protected String email;
    protected String phone;
    protected String login;
    protected String passwd;
    protected Address address;


    // Construtor
    protected Person(Address address)
    {
        this.address = address;
    }


    // GET & SET
    public String getName()
    {
        return name;
    }
    public void setName(String name)
    {
        this.name = name;
    }

    public DateTime getDate_of_birth()
    {
        return date_of_birth;
    }
    public void setDate_of_birth(DateTime date_of_birth)
    {
        this.date_of_birth = date_of_birth;
    }

    public String getDocument()
    {
        return document;
    }
    public void setDocument(String document)
    {
        this.document = document;
    }

    public String getEmail()
    {
        return email;
    }
    public void setEmail(String email)
    {
        this.email = email;
    }

    public String getPhone()
    {
        return phone;
    }
    public void setPhone(String phone)
    {
        this.phone = phone;
    }

    public String getLogin()
    {
        return login;
    }
    public void setLogin(String login)
    {
        this.login = login;
    }
}
