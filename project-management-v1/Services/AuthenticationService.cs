using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace project_management_v1.Services
{
    public class AuthenticationService(UserManager<IdentityUser> userManager, IConfiguration configuration)
        : IAuthenticationService
    {
        public async Task<string> GenerateJwtToken(IdentityUser user)
        {
            var jwtSecret = configuration["ApplicationSettings:JWT_Secret"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // I'm using the same issuer and audience since I'm not integrating with an external service to obtain the token.
            var token = new JwtSecurityToken(
                issuer: "https://localhost:7171",
                audience: "https://localhost:7171",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
    }
}
