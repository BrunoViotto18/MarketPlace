namespace Model;
using DAO;
using Interfaces;
using DTO;

public class Owner : Person, IValidateDataObject<Owner>, IDataController<OwnerDTO, Owner>
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
        {
            owner = new Owner(address);
        }
        return owner;
    }

    public Boolean validateObject(Owner obj) {

        if (this.address == null)
            return false;

        if (this.name == null)
            return false;

        if (this.login == null)
            return false;

        if (this.document == null)
            return false;

        if (this.phone == null)
            return false;

        if (this.email == null)
            return false;

        if (this.date_of_birth == default)
            return false;
        return true;
    }

    public static Owner convertDTOTOModel(OwnerDTO owner)
    {
        Owner modelOwner = new Owner(Address.convertDTOToModel(owner.address));

        modelOwner.name = owner.name;
        modelOwner.email = owner.email;
        modelOwner.date_of_birth = owner.date_of_birth;
        modelOwner.phone = owner.phone;
        modelOwner.document = owner.document;
        modelOwner.login = owner.login;
        modelOwner.passwd = owner.passwd;
       
        return modelOwner;
    }

    public int save()
    {
        var id = 0;

        using(var context = new DaoContext()){
            var owner = new DAO.Owner()
            {
                name = this.name,
                email = this.email,
                date_of_birth = this.date_of_birth,
                phone = this.phone,
                login = this.login,
                passwd = this.passwd,
                document = this.document
            };
            
            context.Owner.Add(owner);
            context.SaveChanges();

            id = owner.id;
        }

        return id;
    }

    public OwnerDTO convertModelToDTO()
    {
        OwnerDTO dtoClient = new OwnerDTO();

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
}
