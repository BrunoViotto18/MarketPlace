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
            endere�o = client.address
        };
    }

    [HttpGet]
    [Route("informations")]
    public void getInformations(int clientID)
    {
        using (var context = new DAO.C)
    }
}
