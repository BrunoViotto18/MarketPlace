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

            if (context.Client.FirstOrDefault(c => c.document == client.document) != null)
                return -1;

            context.Client.Add(client);
            context.SaveChanges();

            id = client.id;
        }

        return id;
    }

    public void delete()
    {
        using (var context = new DAOContext())
        {
            var client = context.Client.FirstOrDefault(s => s.document == this.document);
            var address = context.Address.FirstOrDefault(a => a.street == this.address.getStreet() && a.country == this.address.getCountry() && this.address.getPostalCode() == a.postal_code && a.city == this.address.getCity() && a.state == this.address.getState());

            if (client == null || address == null)
            {
                Console.WriteLine("Anulou :(");
                return;

            }

            context.Address.Remove(address);
            context.Client.Remove(client);
            context.SaveChanges();
        }
    }

    public void update()
    {

    }

    // Retorna o cliente pelo documento
    public static Client findByDocument(string document)
    {
        using var context = new DAO.DAOContext();
        
        return Client.convertDAOToModel(context.Client.Include(c => c.address).Where(c => c.document == document).Single());
    }


    public ClientDTO findById()
    {
        return new ClientDTO();
    }

    public static Client? findByLogin(string login, string senha){

        using var context = new DAOContext();

        var user = context.Client.Include(c => c.address).FirstOrDefault(c => c.login == login && c.passwd == senha);

        if(user == null)
            return null;

        return Client.convertDAOToModel(user);
        
    }

    public List<ClientDTO> getAll()
    {
        List<ClientDTO> client = new List<ClientDTO>();
        return client;
    }

    public static int findId(string login, string senha)
    {
        using (var context = new DAOContext()){
            var user = context.Client.FirstOrDefault(c => c.login == login && c.passwd == senha);

            if(user == null)
                return -1;

            return user.id;
        }
    }
}
