namespace Model;

using Interfaces;
using DTO;

public class Client : Person, IValidateDataObject<Client>, IDataController<ClientDTO, Client>
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

    public Boolean validateObject(Client obj)
    {
        if (name == null)
            return false;

        if (date_of_birth == default)
            return false;

        if (document == null)
            return false;

        if (email == null)
            return false;

        if (phone == null)
            return false;

        if (login == null)
            return false;

        if (address == null)
            return false;

        return true;
    }

    public static Client convertDTOToModel(ClientDTO client)
    {
        Client modelClient = new Client(Address.convertDTOToModel(client.address));

        modelClient.name = client.name;
        modelClient.email = client.email;
        modelClient.date_of_birth = client.date_of_birth;
        modelClient.phone = client.phone;
        modelClient.document = client.document;
        modelClient.login = client.login;
        modelClient.passwd = client.passwd;

        return modelClient;
    }
}