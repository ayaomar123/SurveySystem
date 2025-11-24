using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SurveySystem.Application.Interfaces;

namespace SurveySystem.Infrastructure.Authentication
{
    public class JwtTokenGenerator(IConfiguration configuration) : IJwtTokenGenerator
    {
        public string GenerateToken(Guid userId, string email, string role)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");

            var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]!);

            var signingKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(ClaimTypes.Role, role)
        };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["TokenExpirationInMinutes"]!)),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
