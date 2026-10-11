using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using NorthWind.Membership.Backend.Core.Dtos.UserLogin;
using NorthWind.Membership.Backend.Core.Options;

namespace NorthWind.Membership.Backend.Core.Services;

public class JwtService(IOptions<JwtOptions> options)
{
    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(options.Value.SecurityKey);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private List<Claim> GetClaims(UserDto userDto) =>
    [
        new Claim(ClaimTypes.Name, userDto.Email),
        new Claim("FullName", $"{userDto.FirstName} {userDto.LastName}")
    ];

    private SecurityTokenDescriptor GetTokenDescriptor(
        SigningCredentials signingCredentials,
        List<Claim> claims) =>
        new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = options.Value.ValidIssuer,
            Audience = options.Value.ValidAudience,
            Expires = DateTime.UtcNow.AddMinutes(options.Value.ExpireInMinutes),
            SigningCredentials = signingCredentials
        };

    public string GetToken(UserDto userData)
    {
        var signingCredentials = GetSigningCredentials();
        var claims = GetClaims(userData);
        var tokenDescriptor = GetTokenDescriptor(signingCredentials, claims);
        var tokenHandler = new JsonWebTokenHandler();
        return tokenHandler.CreateToken(tokenDescriptor);
    }
}
