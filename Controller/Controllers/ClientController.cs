using Microsoft.AspNetCore.Mvc;
using DTO;
using Model;

namespace Controller.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    [HttpPost]
    [Route("register")]
    public object registerClient([FromBody] ClientDTO client)
    {
        var clientModel = Client.convertDTOToModel(client);
        var id = clientModel.save();

        return new
        {
            id = id,
            nome = client.name,
            dataDeNascimento = client.date_of_birth,
            documento = client.document,
            email = client.email,
            telefone = client.phone,
            login = client.login,
            senha = client.passwd,
            endereço = client.address
        };
    }

    [HttpGet]
    [Route("informations/{document}")]
    public object getInformations(String document)
    {
        var client = Model.Client.findByDocument(document);
        return client;
    }
}
