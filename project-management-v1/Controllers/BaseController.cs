using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace project_management_v1.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [Authorize]
    public class BaseController(IHttpContextAccessor httpContextAccessor) 
        : ControllerBase
    {
        public ClaimsPrincipal CurrentPrincipal => httpContextAccessor.HttpContext.User;

        public Guid CurrentUserGuidId
        {
            get
            {
                var claim = CurrentPrincipal.Claims.SingleOrDefault(c => c.Type == "sub");
                return claim == null ? Guid.Empty : Guid.Parse(claim.Value);
            }
        }

        public string[] CurrentUserRoles
        {
            get
            {
                var roles = CurrentPrincipal.Claims
                    .Where(x => x.Type == ClaimTypes.Role)
                    .Select(x => x.Value)
                    .ToArray();

                return roles;
            }
        }

    }
}
