using Microsoft.AspNetCore.Mvc;
using DTO;
using Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Text;
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
            endereco = client.address
        };
    }

    [HttpGet]
    [Route("informations/{id}")]
    public IActionResult getInformations(int id)
    {
        var client = Client.findById(id);
        return Ok(client.convertModelToDTO());
    }

    [HttpDelete]
    [Route("removeClient")]
    public void removeOwner([FromBody] ClientDTO request)
    {
        Client.convertDTOToModel(request).delete();
    }



    public IConfiguration _configuration;

    public ClientController(IConfiguration config)
    {
        _configuration = config;
    }

    [HttpPost]
    [Route("login")]
    public IActionResult tokenGenerate([FromBody] LoginDTO login)
    {
        if (login == null || login.login == null || login.passwd == null)
            return BadRequest("Empty credentials");

        var user = Model.Client.findByLogin(login.login, login.passwd);
        Response.Headers.Add("Access-Control-Allow-Origin", "*");

        if(user == null)
            return BadRequest("Invalid credentials");

        var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("Id", Model.Client.findId(user.getLogin(), user.getPasswd()).ToString())
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["JwtAudience"],
            claims,
            expires: DateTime.UtcNow.AddYears(1),
            signingCredentials: signIn);

        return Ok(new JwtSecurityTokenHandler().WriteToken(token));
    }
}
