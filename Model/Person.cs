namespace Model;
using DTO;

public class Person
{
    // Atributos
    protected int id;
    protected String name;
    protected DateTime date_of_birth;
    protected String document;
    protected String email;
    protected String phone;
    protected String login;
    protected String passwd;

    protected Address address;


    // Construtor
    protected Person(int id, Address address)
    {
        this.id = id;
        this.address = address;
    }

    protected Person(Address address)
    {
        this.address = address;
    }


    // GET & SET
    public int getId()
    {
        return id;
    }

    public String getName()
    {
        return name;
    }
    public void setName(String name)
    {
        this.name = name;
    }

    public DateTime getDateOfBirth()
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

    public String getPasswd()
    {
        return passwd;
    }
    public void setPasswd(String passwd)
    {
        this.passwd = passwd;
    }
}
