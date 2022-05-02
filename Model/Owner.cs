﻿namespace Model;
using DAO;
using Interfaces;
using DTO;

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
        {
            owner = new Owner(address);
        }
        return owner;
    }

    public Boolean validateObject() {

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

        if (this.passwd == null)
            return false;

        return true;
    }

    public static Owner convertDTOToModel(OwnerDTO owner)
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
                email = this.email,
                date_of_birth = this.date_of_birth,
                phone = this.phone,
                login = this.login,
                passwd = this.passwd,
                document = this.document,
                address = addressDao
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

    public static Owner convertDAOToModel(DAO.Owner owner)
    {
        return new Owner(Address.convertDAOToModel(owner.address))
        {
            name = owner.name,
            date_of_birth = owner.date_of_birth,
            document = owner.document, 
            email = owner.email,    
            phone = owner.phone,
            login=owner.login,
            passwd = owner.passwd

        };
    }

    public void delete()
    {

    }

    public void update()
    {

    }

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
