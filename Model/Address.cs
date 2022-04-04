namespace Model;

public class Address
{
    // Atributos
    private String street;
    private String city;
    private String state;
    private String country;
    private String poste_code;

    // Construtor
    public Address(String street, String city, String state, String country, String poste_code)
    {
        this.street = street;
        this.city = city;
        this.state = state;
        this.country = country;
        this.poste_code = poste_code;
    }

    // GET & SET
    public String getStreet()
    {
        return street;
    }
    public void setStreet(String street)
    {
        this.street = street;
    }

    public String getCity()
    {
        return street;
    }
    public void setCity(String city)
    {
        this.city = city;
    }

    public String getState()
    {
        return street;
    }
    public void setState(String state)
    {
        this.state = state;
    }

    public String getCountry()
    {
        return street;
    }
    public void setCountry(String country)
    {
        this.country = country;
    }

    public String getPostalCode()
    {
        return poste_code;
    }
    public void setPostalCode(String poste_code)
    {
        this.poste_code = poste_code;
    }
}
