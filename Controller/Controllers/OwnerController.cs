using Microsoft.AspNetCore.Mvc;
using DTO;
using Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            endereco = owner.address
        };
    }

    [HttpGet]
    [Route("informations/{id}")]
    public IActionResult getInformations(int id)
    {
        var owner = Owner.findById(id);
        return Ok(owner.convertModelToDTO());
    }

    [HttpGet]
    [Route("getById/{id}")]
    public object getById(int id)
    {
        var owner = Owner.findID(id);
        return owner;
    }


    

    [HttpDelete]
    [Route("removeOwner")]
    public void removeOwner([FromBody] OwnerDTO request)
    {
        Owner.convertDTOToModel(request).delete();
    }


    public IConfiguration _configuration;
    public OwnerController(IConfiguration config)
    {
        _configuration = config;
    }
    [HttpPost]
    [Route("login")]
    public IActionResult tokenGenerate([FromBody] LoginDTO login)
    {
        if (login == null || login.login == null || login.passwd == null)
            return BadRequest("Empty credentials");

        var user = Model.Owner.findByLogin(login.login, login.passwd);
        Response.Headers.Add("Access-Control-Allow-Origin", "*");

        if (user == null)
            return BadRequest("Invalid credentials");

        var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("Id", Model.Owner.findByLogin(user.getLogin(), user.getPasswd()).getId().ToString()),
                new Claim("Client", "false")
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
