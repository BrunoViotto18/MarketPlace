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
    [Route("informations/{document}")]
    public object getInformations(String document)
    {
        return Client.findByDocument(document);
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
    public IActionResult tokenGenerate([FromBody] ClientDTO login)
    {
        if (login == null || login.login == null || login.passwd == null)
            return BadRequest("Empty credentials");

        var user = Model.Client.findLogin(login);
        Response.Headers.Add("Access-Control-Allow-Origin", "*");

        if(user == null)
            return BadRequest("Invalid credentials");

        var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("UserId", Model.Client.findId(login.login).ToString()),
                new Claim("UserName", user.name),
                new Claim("Email", user.email)
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["JwtAudience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: signIn);

        return Ok(new ObjectResult(
            new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                id = Client.findId(user.login)
            })
        );
    }
}
