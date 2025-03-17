using project_management_v1.Application.DTOs;
using System.Threading.Tasks;

namespace project_management_v1.Application.Repository
{
    public interface IProjectManagementRepository
    {
        Task<Result<List<ProjectDTo>>> GetAllProjects(string[] currentUserRoles);
        Task<Result> AssignRoleToProject(AssignRoleToProjectDTo request);
    }
}
