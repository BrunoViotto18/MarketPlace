using Microsoft.AspNetCore.Mvc;
using DTO;
using Model;

namespace Controller.Controllers;

[ApiController]
[Route("[controller]")]
public class OwnerController : ControllerBase
{
    [HttpPost]
    [Route("register")]
    public object registerOwner([FromBody] OwnerDTO owner)
    {
        var ownerModel = Owner.convertDTOToModel(owner);
        var id = ownerModel.save();

        return new
        {
            id = id,
            nome = owner.name,
            dataDeNascimento = owner.date_of_birth,
            documento = owner.document,
            email = owner.email,
            telefone = owner.phone,
            login = owner.login,
            senha = owner.passwd,
            endereço = owner.address
        };
    }

    [HttpGet]
    [Route("informations/{document}")]
    public OwnerDTO getInformations(String document)
    {
        return Owner.findByDocument(document);
    }
}
