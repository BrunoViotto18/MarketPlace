namespace Model;
using DAO;
using Interfaces;
using DTO;

public class Address : IValidateDataObject, IDataController<AddressDTO, Address>
{
    // Atributos
    private int id;
    private String street;
    private String city;
    private String state;
    private String country;
    private String poste_code;


    // Construtor
    public Address(int id, String street, String city, String state, String country, String poste_code)
    {
        this.id = id;
        this.street = street;
        this.city = city;
        this.state = state;
        this.country = country;
        this.poste_code = poste_code;
    }

    public Address(String street, String city, String state, String country, String poste_code)
    {
        this.street = street;
        this.city = city;
        this.state = state;
        this.country = country;
        this.poste_code = poste_code;
    }


    // GET & SET
    public int getId()
    {
        return id;
    }

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

    // Valida se o objeto tem todos os seus campos diferente de nulo
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


    /* Conversores */

    // Converte um objeto DTO para Model
    public static Address convertDTOToModel(AddressDTO address)
    {
        return new Address(
            address.street,
            address.city,
            address.state,
            address.country,
            address.postal_code
        );
    }

    // Converte um objeto Model para DTO
    public AddressDTO convertModelToDTO()
    {
        return new AddressDTO
        {
            street = this.street,
            state = this.state,
            city = this.city,
            country = this.country,
            postal_code = this.poste_code
        };
    }

    // Converte um objeto DAO para Model
    public static Address convertDAOToModel(DAO.Address address)
    {
        return new Address(
            address.id,
            address.street,
            address.city,
            address.state,
            address.country,
            address.postal_code
        );
    }


    /* Métodos SQL */

    // Salva o objeto atual no banco de dados
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

            if (context.Address
                .FirstOrDefault(
                    a => a.street == address.street &&
                    a.city == address.city &&
                    a.state == address.state &&
                    a.country == address.country &&
                    a.postal_code == address.postal_code
                ) != null)
                return -1;

            context.Address.Add(address);
            context.SaveChanges();

            id = address.id;
        }

        return id;
    }


    public void delete()
    {
        using(var context = new DAOContext())
        {
            var address = context.Address.FirstOrDefault(a => a.street == this.street && a.country == this.country && this.poste_code == a.postal_code && a.city == this.city && a.state == this.state);

            if (address == null)
                return;
            context.Address.Remove(address);
            context.SaveChanges();
        }
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
