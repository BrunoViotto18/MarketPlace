namespace Controller.Controllers;

using System.IdentityModel.Tokens.Jwt;

public class JWT
{
    public static int GetIdFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var json = handler.ReadJwtToken(token.Replace("Bearer ", ""));

        Console.WriteLine(json.Claims.First(c => c.Type == "Id").Value);
        return int.Parse(json.Claims.First(c => c.Type == "Id").Value);
    }
}
