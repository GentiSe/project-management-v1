using project_management_v1.Application.DTOs;

namespace project_management_v1.Application.Repository
{
    public interface IProjectManagementRepository
    {
        Task<List<ProjectDTo>> GetAllProjects();
    }
}
