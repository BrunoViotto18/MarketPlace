namespace Model;

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

    public Boolean validateObject(Owner obj){

        if(this.address ==  null)
            return false;

        if(this.name == null)
            return false;

        if(this.login == null)
            return false;

        if(this.document == null)
            return false;

        if(this.phone == null)
            return false;

        if(this.email==null)
            return false;

        if(this.date_of_birth == default)
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
}
