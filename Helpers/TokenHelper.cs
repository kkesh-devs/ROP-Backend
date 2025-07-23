using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KKESH_ROP.DTO.Helpers;
using Microsoft.IdentityModel.Tokens;

namespace KKESH_ROP.Helpers;

public class TokenHelper(IConfiguration config)
{
    public string GenerateToken(TokenDto user)
    {
        var claims = new List<Claim>
        {
            new("_id", user._id),
            new("Email", user.Email),
            new("Role", user.Role),
        };

        var jwtKey = config["Jwt:Key"] 
                     ?? throw new InvalidOperationException("JWT key not found in configuration.");
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: credentials,
            expires: DateTime.Now.AddHours(24),
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"]
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
