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
            name = client.name,
            date_of_birth = client.date_of_birth,
            document = client.document,
            email = client.email,
            phone = client.phone,
            login = client.login,
            passwd = client.passwd,
            address = client.address
        };
    }

    [HttpGet]
    [Route("information")]
    public void getInformations()
    {

    }
}
