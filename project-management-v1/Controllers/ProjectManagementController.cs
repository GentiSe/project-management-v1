using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_management_v1.Application.Domain.Constants;
using project_management_v1.Application.DTOs;
using project_management_v1.Application.Repository;

namespace project_management_v1.Controllers
{
    [Route("api/v1/projects")]
    [ApiController]
    public class ProjectManagementController(IProjectManagementRepository repository
        , IHttpContextAccessor httpContextAccessor)
        : BaseController(httpContextAccessor)
    {

        [HttpGet("all")]
        [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Analyst}, {UserRoles.CategoryManager}, {UserRoles.Basic}")]
        public async Task<IActionResult> GetAllProjects()
        {
            if (CurrentUserRoles.Length == 0)
                return Forbid("You're not allowed to authorize this resource.");

            var result = await repository.GetAllProjects(CurrentUserRoles);

            if (!result.IsSuccess)
            {
                // Currently returning only a BadRequest, which might not be suitable for all cases.
                // Could extend the Result class to include an ErrorCode and map it to an appropriate HTTP response code
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        /// <summary>
        /// This endpoint assigns specific roles to projects.
        /// Only users with the "Admin" role are authorized to assign roles to projects.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRoleToProject([FromBody] AssignRoleToProjectDTo request)
        {
            if (request == null)
                return BadRequest("Data must be provided.");

            var result = await repository.AssignRoleToProject(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Created();
        }
    }
}
