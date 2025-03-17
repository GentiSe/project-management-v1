using Microsoft.AspNetCore.Identity;

namespace project_management_v1.Services
{
    public interface IAuthenticationService
    {
        Task<string> GenerateJwtToken(IdentityUser user);
    }
}
