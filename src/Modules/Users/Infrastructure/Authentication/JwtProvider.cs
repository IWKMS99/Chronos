using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Chronos.Modules.Users.Application;
using Chronos.Modules.Users.Application.Services;
using Chronos.Modules.Users.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Chronos.Modules.Users.Infrastructure.Authentication;

public class JwtProvider : IJwtProvider {
    private readonly IConfiguration _configuration;

    public JwtProvider(IConfiguration configuration) {
        _configuration = configuration;
    }

    public TokenResponse Generate(User user) {
        var claims = new Claim[] {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new("role", user.Role)
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)),
            SecurityAlgorithms.HmacSha256
        );

        var accessToken = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            null,
            DateTime.UtcNow.AddMinutes(15),
            signingCredentials
        );

        var accessTokenValue = new JwtSecurityTokenHandler().WriteToken(accessToken);

        var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        return new TokenResponse(accessTokenValue, refreshToken);
    }
}