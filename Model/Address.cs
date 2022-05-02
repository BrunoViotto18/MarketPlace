namespace Model;
using DAO;
using Interfaces;
using DTO;

public class Address : IValidateDataObject, IDataController<AddressDTO, Address>
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
        return city;
    }
    public void setCity(String city)
    {
        this.city = city;
    }

    public String getState()
    {
        return state;
    }
    public void setState(String state)
    {
        this.state = state;
    }

    public String getCountry()
    {
        return country;
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


    // Métodos

    // Valida se um objeto tem todos os seus campos diferente de nulo
    public Boolean validateObject()
    {
        if (this.street == null)
            return false;

        if (this.city == null)
            return false;

        if (this.state == null)
            return false;

        if (this.country == null)
            return false;

        if (this.poste_code == null)
            return false;

        return true;
    }

    public static Address convertDTOToModel(AddressDTO address)
    {
        var modelAddress = new Address(
            address.street,
            address.city,
            address.state,
            address.country,
            address.postal_code
        );

        return modelAddress;
    }

    public int save()
    {
        int id;

        using (var context = new DAOContext())
        {
            var address = new DAO.Address
            {
                street = this.street,
                city = this.city,
                state = this.state,
                country = this.country,
                postal_code = this.poste_code
            };

            context.Address.Add(address);
            context.SaveChanges();

            id = address.id;
        }

        return id;
    }

    public AddressDTO convertModelToDTO()
    {
        AddressDTO addressDTO = new AddressDTO();

        addressDTO.street = this.street;    
        addressDTO.state = this.state;
        addressDTO.city = this.city;    
        addressDTO.country = this.country;
        addressDTO.postal_code = this.poste_code;

        return addressDTO;
    }

    public static Address convertDAOToModel(DAO.Address address)
    {
        return new Address(address.street, address.city, address.state, address.country, address.postal_code);
    }

    public void delete()
    {

    }

    public void update()
    {

    }

    public AddressDTO findById()
    {

        return new AddressDTO();
    }

    public List<AddressDTO> getAll()
    {
        List<AddressDTO> addresses = new List<AddressDTO>();
        return addresses;
    }
}
