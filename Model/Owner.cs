namespace Model;
using Interfaces;

public class Owner : Person, IValidateDataObject<Owner>
{
    // Atributos
    private static Owner owner;


    private Guid uuid = Guid.NewGuid();
    // Construtor
    private Owner(Address address) : base(address)
    {

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
}
