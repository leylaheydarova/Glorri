using Glorri.API.Models;
using Glorri.API.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Glorri.API.Services.Implements
{
    public class TokenService : ITokenService
    {
        readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAccessToken(AppUser user)
        {
            var issuer = _configuration["Jwt:issuer"];
            var audience = _configuration["Jwt:audience"];
            int.TryParse(_configuration["Jwt:expiresIn"], out int expiresIn);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                audience: audience,
                issuer: issuer,
                expires: DateTime.UtcNow.AddHours(4).AddHours(expiresIn),
                claims: claims,
                signingCredentials: creds
                );

            var handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
        }
    }
}
