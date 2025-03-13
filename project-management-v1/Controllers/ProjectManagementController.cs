using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project_management_v1.Application.Repository;

namespace project_management_v1.Controllers
{
    [Route("api/v1/projects")]
    [ApiController]
    public class ProjectManagementController(IProjectManagementRepository repository)
        : ControllerBase
    {

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllProjects()
        {
            var result = await repository.GetAllProjects();
            return Ok(result);
        }
    }
}
