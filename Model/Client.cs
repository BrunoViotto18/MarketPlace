namespace Model;

using Microsoft.EntityFrameworkCore;
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
        if (instance == null)
            instance = new Client(address);

        return instance;
    }

    // Valida se o objeto tem todos os seus campos diferente de nulo
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

        if (this.passwd == null)
            return false;

        if (this.address == null)
            return false;

        return true;
    }


    /* Conversores */

    // Converte um objeto DTO para Model
    public static Client convertDTOToModel(ClientDTO client)
    {
        return new Client(Address.convertDTOToModel(client.address))
        {
            name = client.name,
            date_of_birth = client.date_of_birth,
            document = client.document,
            email = client.email,
            phone = client.phone,
            login = client.login,
            passwd = client.passwd
        };
    }

    // Converte um objeto Model para DTO
    public ClientDTO convertModelToDTO()
    {
        return new ClientDTO
        {
            name = this.name,
            date_of_birth = this.date_of_birth,
            document = this.document,
            email = this.email,
            phone = this.phone,
            login = this.login,
            passwd = this.passwd,
            address = this.address.convertModelToDTO()
        };
    }

    // Converte um objeto DAO para Model
    public static Client convertDAOToModel(DAO.Client client)
    {
        return new Client(Address.convertDAOToModel(client.address))
        {
            name = client.name,
            date_of_birth = client.date_of_birth,
            document = client.document,
            email = client.email,
            phone = client.phone,
            login = client.login,
            passwd = client.passwd
        };
    }


    /* Métodos SQL */

    // Salva o objeto atual no banco de dados
    public int save()
    {
        int id;

        using (var context = new DAOContext())
        {
            var addressDao = context.Address.FirstOrDefault(
                a => a.street == address.getStreet() &&
                a.city == address.getCity() &&
                a.state == address.getState() &&
                a.country == address.getCountry() &&
                a.postal_code == address.getPostalCode());

            if (addressDao == null)
                return -1;

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

            if (context.Client.FirstOrDefault(c => c.document == client.document) != null)
                return -1;

            context.Client.Add(client);
            context.Entry(client.address).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            context.SaveChanges();

            id = client.id;
        }

        return id;
    }


    public void delete()
    {

    }


    public void update()
    {

    }

    // Retorna o cliente pelo documento
    public static ClientDTO findByDocument(string document)
    {
        using (var context = new DAO.DAOContext())
        {
            var client = context.Client.Include(c => c.address).Where(c => c.document == document).Single();
            return Client.convertDAOToModel(client).convertModelToDTO();
        }
    }


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
