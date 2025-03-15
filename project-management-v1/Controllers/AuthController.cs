using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using project_management_v1.Application.Requests;
using project_management_v1.Infrastructure.Services;

namespace project_management_v1.Controllers
{
    [Route("api/authenticate")]
    [ApiController]
    public class AuthController(UserManager<IdentityUser> userManager
        , SignInManager<IdentityUser> signInManager
        , IAuthenticationService authService) : ControllerBase
    {

        [HttpPost]
        
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequestModel request)
        {
            if (request is null)
            {
                return BadRequest("Data must be provided");
            }

            if (string.IsNullOrEmpty(request.Email) && string.IsNullOrEmpty(request.Username))
            {
                return BadRequest("Email or Username must be provided.");
            }

            // Find user by email or username
            var user = !string.IsNullOrEmpty(request.Email)
                ? await userManager.FindByEmailAsync(request.Email)
                : await userManager.FindByNameAsync(request.Username);

            if (user == null 
                || !(await signInManager.PasswordSignInAsync(user, request.Password, false, false)).Succeeded)
            {
                return Unauthorized("Invalid email or password");
            }

            var token = await authService.GenerateJwtToken(user);
            return Ok(new { Token = token });
        }
    }
}
