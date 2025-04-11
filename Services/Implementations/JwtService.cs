using JediArchives.DataStorage.EfModels;
using JediArchives.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JediArchives.Services.Implementations;
public class JwtService : IJwtService {
    private readonly JwtSettings _settings;

    public JwtService(IOptions<JwtSettings> options) {
        _settings = options.Value;
    }

    /// <summary>
    /// Generates a signed JWT token containing the user's ID, name, and rank.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="userName">The name of the user.</param>
    /// <param name="rank">The rank of the user, used as a role claim.</param>
    /// <returns>A signed JWT token string containing user identity and authorization claims.</returns>
    public string GenerateToken(int userId, string userName, JediRanks rank) {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.Role, rank.ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_settings.ExpireMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public class JwtSettings {
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int ExpireMinutes { get; set; }
}
