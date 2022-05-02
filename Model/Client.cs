namespace Model;

using Interfaces;
using DTO;
using DAO;

public class Client : Person, IValidateDataObject, IDataController<ClientDTO, Client>
{
    // Atributos
    private Guid uuid;

    private static Client instance;


    // Construtor
    public Client(Address address) : base(address)
    {
        this.uuid = Guid.NewGuid();
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
    public static Client getInstance(Address address)
    {
        if(instance == null)
        {
            instance = new Client(address);
        }
        return instance;
    }

    public Boolean validateObject()
    {
        if (this.name == null)
            return false;

        if (this.date_of_birth == default)
            return false;

        if (this.document == null)
            return false;

        if (this.email == null)
            return false;

        if (this.phone == null)
            return false;

        if (this.login == null)
            return false;

        if (this.address == null)
            return false;

        if (this.passwd == null)
            return false;

        return true;
    }

    public static Client convertDTOToModel(ClientDTO client)
    {
        Client modelClient = new Client(Address.convertDTOToModel(client.address))
        {
            name = client.name,
            email = client.email,
            date_of_birth = client.date_of_birth,
            phone = client.phone,
            document = client.document,
            login = client.login,
            passwd = client.passwd
        };

        return modelClient;
    }

    public int save()
    {
        int id;

        using (var context = new DAOContext())
        {
            var addressDao = new DAO.Address
            {
                street = this.address.getStreet(),
                city = this.address.getCity(),
                state = this.address.getState(),
                country = this.address.getCountry(),
                postal_code = this.address.getPostalCode()
            };

            var client = new DAO.Client
            {
                name = this.name,
                email = this.email,
                date_of_birth = this.date_of_birth,
                phone = this.phone,
                login = this.login,
                passwd = this.passwd,
                document = this.document,
                address = addressDao
            };

            context.Client.Add(client);
            context.SaveChanges();

            id = client.id;
        }

        return id;
    }

    public ClientDTO convertModelToDTO()
    {
        ClientDTO dtoClient = new ClientDTO();

        dtoClient.name = this.name;
        dtoClient.date_of_birth = this.date_of_birth;
        dtoClient.document = this.document;
        dtoClient.email = this.email;
        dtoClient.phone = this.phone;
        dtoClient.login = this.login;
        dtoClient.passwd = this.passwd;
        dtoClient.address = this.address.convertModelToDTO();

        return dtoClient;
    }

    public void delete()
    {

    }

    public void update()
    {

    }

   /* public ClientDTO find(string document)
    {
        using (var context = new DAO.DAOContext())
        {
            var client = context.Client.Where(c => c.document == document).Single();


        }
    }*/

    public ClientDTO findById()
    {
        return new ClientDTO();
    }

    public List<ClientDTO> getAll()
    {
        List<ClientDTO> client = new List<ClientDTO>();
        return client;
    }


}
