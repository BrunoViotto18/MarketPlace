namespace Model;
using DAO;
using Interfaces;
using DTO;
using Microsoft.EntityFrameworkCore;

public class Owner : Person, IValidateDataObject, IDataController<OwnerDTO, Owner>
{
    // Atributos
    private Guid uuid;

    private static Owner owner;


    // Construtor
    private Owner(Address address) : base(address)
    {
        uuid = Guid.NewGuid();
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
    public static Owner getInstance(Address address)
    {
        if (owner == null)
            owner = new Owner(address);

        return owner;
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
    public static Owner convertDTOToModel(OwnerDTO owner)
    {
        return new Owner(Address.convertDTOToModel(owner.address))
        {
            name = owner.name,
            date_of_birth = owner.date_of_birth,
            document = owner.document,
            email = owner.email,
            phone = owner.phone,
            login = owner.login,
            passwd = owner.passwd
        };
    }

    // Converte um objeto Model para DTO
    public OwnerDTO convertModelToDTO()
    {
        return new OwnerDTO
        {
            id = this.id,
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
    public static Owner convertDAOToModel(DAO.Owner owner)
    {
        return new Owner(Address.convertDAOToModel(owner.address))
        {
            id = owner.id,
            name = owner.name,
            date_of_birth = owner.date_of_birth,
            document = owner.document,
            email = owner.email,
            phone = owner.phone,
            login = owner.login,
            passwd = owner.passwd
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

            var owner = new DAO.Owner()
            {
                name = this.name,
                date_of_birth = this.date_of_birth,
                document = this.document,
                email = this.email,
                phone = this.phone,
                login = this.login,
                passwd = this.passwd,
                address = addressDao
            };

            if (context.Owner.FirstOrDefault(o => o.document == owner.document) != null)
                return -1;
            
            context.Owner.Add(owner);
            context.SaveChanges();

            id = owner.id;
        }

        return id;
    }


    public void delete()
    {
        using(var context = new DAOContext())
        {
            var owner = context.Owner.FirstOrDefault(s => s.document == this.document);
            var address = context.Address.FirstOrDefault(a => a.street == this.address.getStreet() && a.country == this.address.getCountry() && this.address.getPostalCode() == a.postal_code && a.city == this.address.getCity() && a.state == this.address.getState());

            if (owner == null || address == null)
                return;
                
            context.Address.Remove(address);
            context.Owner.Remove(owner);
            context.SaveChanges();
        }
    }

    public void update()
    {

    }

    public static Owner? findByLogin(string login, string senha)
    {

        using var context = new DAOContext();

        var user = context.Owner.Include(o => o.address).FirstOrDefault(o => o.login == login && o.passwd == senha);

        if (user == null)
            return null;

        return Owner.convertDAOToModel(user);
    }

    // Retorna o ID do objeto atual
    public int findId()
    {
        using var context = new DAOContext();
        
        return context.Owner.Where(o => o.document == this.document).Single().id;
    }

    // Retorna o dono por documento
    public static OwnerDTO findByDocument(string document)
    {
        using (var context = new DAO.DAOContext())
        {
            var owner = context.Owner.Include(c => c.address).Where(c => c.document == document).Single();
            return Owner.convertDAOToModel(owner).convertModelToDTO();
        }
    }

    public static Owner findById(int id)
    {
        using var context = new DAOContext();

        return Owner.convertDAOToModel(context.Owner.Include(o => o.address).FirstOrDefault(o => o.id == id));
    }

    // Trash

    public OwnerDTO findById()
    {
        return new OwnerDTO();
    }


    public List<OwnerDTO> getAll()
    {
        List<OwnerDTO> client = new List<OwnerDTO>();
        return client;
    }
}
