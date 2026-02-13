using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RetailPulseAI.Domain.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RetailPulseAI.API.Controllers;

public sealed record LoginRequest(string Email, string Role, string TenantId, string TenantSlug);

[ApiController]
[Route("api/auth")]
public sealed class AuthController(IOptions<JwtOptions> jwtOptions) : ControllerBase
{
    [HttpPost("token")]
    public IActionResult CreateToken([FromBody] LoginRequest request)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SigningKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, request.Email),
            new(ClaimTypes.Email, request.Email),
            new(ClaimTypes.Role, Enum.TryParse<UserRole>(request.Role, true, out var role) ? role.ToString() : UserRole.Viewer.ToString()),
            new("tenant_id", request.TenantId),
            new("tenant_slug", request.TenantSlug)
        };

        var token = new JwtSecurityToken(
            issuer: jwtOptions.Value.Issuer,
            audience: jwtOptions.Value.Audience,
            expires: DateTime.UtcNow.AddHours(8),
            claims: claims,
            signingCredentials: creds);

        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }
}

public sealed class JwtOptions
{
    public const string SectionName = "Jwt";

    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string SigningKey { get; set; } = string.Empty;
}
