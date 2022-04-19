using NUnit.Framework;
using Model;
using DTO;

namespace testesAutomatizados
{

    public class TestDAO 
    {
        [Test]
        public void insertAddress()
        {
            var id=0;

            var addressDTO  =  new AddressDTO();

            addressDTO.street = "rua 1";

            addressDTO.state = "estado 1";

            addressDTO.city  = "cidade 1";

            addressDTO.country = "pais 1";

            addressDTO.postal_code = "12545215";

            var addressModel = Address.convertDTOToModel(addressDTO);

            if(addressModel.validateObject()){
                id = addressModel.save();
            }

            Assert.That(id, Is.Not.EqualTo(0));
        }

        [Test]
        public void insertClient()
        {
            var id = 0;

            var addressDTO  =  new AddressDTO();

            addressDTO.street = "rua cliente 1";

            addressDTO.state = "estado cliente 1";

            addressDTO.city  = "cidade cliente 1";

            addressDTO.country = "pais cliente 1";

            addressDTO.postal_code = "12cliente5";


            var clientDTO = new ClientDTO();

            clientDTO.name = "Aldo Batista";

            clientDTO.email = "aldo.batista@email.com";

            clientDTO.login = "aldo.batista@email.com";

            clientDTO.address = addressDTO;

            clientDTO.passwd = "sdfsdgfgd";

            clientDTO.phone = "41999999999";

            var clientModel = Client.converteDTOToModel(clientDTO);

            if(clientModel.validateObject()){
                id = clientModel.save();
            }

            Assert.That(id, Is.Not.EqualTo(0));
        }

        [Test]
        public void insertOwner()
        {
            var id = 0;

            var addressDTO  =  new AddressDTO();

            addressDTO.street = "rua owner 1";

            addressDTO.state = "estado owner 1";

            addressDTO.city  = "cidade owner 1";

            addressDTO.country = "pais owner 1";

            addressDTO.postal_code = "12owner5";


            var ownerDTO = new OwnerDTO();

            ownerDTO.name = "Carlos Ribeiro";

            ownerDTO.email = "carlos.ribeiro@email.com";

            ownerDTO.login = "carlos.ribeiro@email.com";

            ownerDTO.address = addressDTO;

            ownerDTO.passwd = "sdfsdgfgd";

            ownerDTO.phone = "41999999999";

            var ownerModel = Owner.converteDTOToModel(ownerDTO);

            if(ownerModel.validateObject()){
                id = ownerModel.save();
            }

            Assert.That(id, Is.Not.EqualTo(0));

        }

        
    }
}