using Microsoft.AspNetCore.Mvc;
using DTO;
using Model;

namespace Controller.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressController : ControllerBase
{
    [HttpPost]
    [Route("register")]
    public object registerAddress([FromBody] AddressDTO address)
    {
        var addressModel = Address.convertDTOToModel(address);
        var id = addressModel.save();

        return new
        {
            id = id,
            rua = address.street,
            cidade = address.city,
            estado = address.state,
            pais = address.country,
            codigoPostal = address.postal_code
        };
    }

    [HttpDelete]
    public void removeAddress(AddressDTO address)
    {

    }

    [HttpPut]
    public void updateAddress(AddressDTO address)
    {

    }
}
