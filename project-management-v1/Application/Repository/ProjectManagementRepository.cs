using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using project_management_v1.Application.Domain.Constants;
using project_management_v1.Application.Domain.Entities;
using project_management_v1.Application.Domain.Enums;
using project_management_v1.Application.DTOs;
using project_management_v1.Infrastructure.Data;
using project_management_v1.Infrastructure.Helpers;

namespace project_management_v1.Application.Repository
{
    public class ProjectManagementRepository(ApplicationDbContext context
        , RoleManager<IdentityRole> roleManager) 
        : IProjectManagementRepository
    {
        public async Task<Result> AssignRoleToProject(AssignRoleToProjectDTo request)
        {
            var role = await roleManager.FindByNameAsync(request.Role);

            if (role == null)
                return Result.Failure("Requested Role not found.");

            var result = await ValidateRequest(request, role.Id);

            if (!result.IsSuccess)
                return result;

            var accessType = ProjectHelpers.GetAccesTypeForRole(request.Role);

            var roleAccess = new ProjectRoleAccess
            {
                ProjectId = request.ProjectId,
                AccesType = accessType,
                RoleId = role.Id,
                IsActive = true
            };

            await context.ProjectRoleAccesses.AddAsync(roleAccess);
            await context.SaveChangesAsync();

            return Result.Success();
        }

        private async Task<Result> ValidateRequest(AssignRoleToProjectDTo request, string roleId)
        {
            var project = await context.Projects.FirstOrDefaultAsync(x => x.Id == request.ProjectId && !x.IsDeleted);
            if (project == null)
                return Result.Failure("Project not found.");

            var existingAccess = await context.ProjectRoleAccesses
                    .FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId && p.RoleId == roleId);

            if (existingAccess != null)
                return Result.Failure("Role already assigned to this project.");

            return Result.Success();
        }

        public async Task<List<ProjectDTo>> GetAllProjects(string[] currentUserRoles)
        {
            var roleIds = await GetRoleIds(currentUserRoles);

            // Using eager loading with `Include` for related `Children` and `Items`. 
            // Perform the query directly to retrieve the projects with their related entities.

            // NOTE: This solution loads all the projects into memory at once, which may not be ideal for very large datasets. 
            // In such cases, i would consider using pagination, filtering, or another strategy to handle large amounts of data efficiently.
            var projects = await context.Projects
                .AsNoTracking()
                .Include(x => x.Items)
                .Include(x => x.ProjectRoleAccesses)
                .Include(x => x.Children.Where(c => !c.IsDeleted))
                    .ThenInclude(x => x.Items.Where(c => !c.IsDeleted))
                .Where(x => !x.IsDeleted
                    && x.ProjectRoleAccesses.Any(pra => roleIds.Contains(pra.RoleId) 
                    && pra.IsActive
                    && (pra.AccesType == AccesType.Write || pra.AccesType == AccesType.Read)))
                .Select(x => new ProjectDTo
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description =x.Description,
                    CreatedAt = x.CreatedAt,
                    ParentId = x.ParentId,
                    Items = x.Items.Select(x => new ProjectItemDTo
                    {
                        Id = x.Id,
                        AssigneId = x.AssigneId,
                        ReporterId = x.ReporterId,
                        CreatedAt =x.CreatedAt,
                        Order= x.Order,
                        Priority = x.Priority,
                        Text = x.Text,
                        Time = x.Time,
                        Title = x.Title,
                        Url = x.Url,
                    }).ToList(),
                    Children = new List<ProjectDTo>()
                })
                .ToListAsync();
            
            if (!projects.Any())
            {
                return []; // just return an empty list.
            }
            var projectLookup =  projects.ToDictionary(x => x.Id);

            var finalResult = BuildProjectHierarchyFromList(projectLookup, projects);
            return finalResult;
        }

        private async Task<List<string>> GetRoleIds(string[] currentUserRoles)
        {
            // Retrieve RoleIds for the current user roles,
            // as the relationship in the database is based on primary keys.
            var roleIds = await roleManager.Roles.Where(x => currentUserRoles.Contains(x.Name))
                    .Select(x => x.Id).ToListAsync();

            return roleIds;
        }

        /// <summary>
        /// Builds the project hierarchy using a Dictionary for O(1) parent-child lookups, 
        /// offering better performance than recursion for large datasets.
        /// </summary>
        /// <param name="projectLookup"></param>
        /// <param name="projects"></param>
        /// <returns></returns>
        private List<ProjectDTo> BuildProjectHierarchyFromList(Dictionary<Guid, ProjectDTo> projectLookup
            , List<ProjectDTo> projects)
        {
            List<ProjectDTo> rootProjects = [];

            foreach (var project in projects)
            {
                // Check if project has parentId and add it as a children to the ParentObject.
                if (project.ParentId.HasValue 
                    && projectLookup.TryGetValue(project.ParentId.Value, out var parentProject))
                {
                        parentProject.Children.Add(project);
                }
                else
                {
                    rootProjects.Add(project);
                }
            }
            return rootProjects;
        }
    }
}
