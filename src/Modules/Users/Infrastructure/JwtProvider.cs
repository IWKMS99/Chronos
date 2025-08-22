using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Chronos.Users.Domain;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Chronos.Users.Infrastructure;

public record TokenResponse(string AccessToken);

public class JwtProvider {
    private readonly JwtOptions _options;

    public JwtProvider(IOptions<JwtOptions> options) {
        _options = options.Value;
    }

    public TokenResponse Generate(User user) {
        var claims = new Claim[] {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new("role", user.Role)
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key)),
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            null,
            DateTime.UtcNow.AddHours(1),
            signingCredentials
        );

        string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        return new TokenResponse(tokenValue);
    }
}