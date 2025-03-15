using Microsoft.AspNetCore.Identity;

namespace project_management_v1.Infrastructure.Services
{
    public interface IAuthenticationService
    {
        Task<string> GenerateJwtToken(IdentityUser user);
    }
}
