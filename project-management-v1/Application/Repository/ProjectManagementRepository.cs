using Microsoft.EntityFrameworkCore;
using project_management_v1.Application.DTOs;
using project_management_v1.Infrastructure.Data;

namespace project_management_v1.Application.Repository
{
    public class ProjectManagementRepository(ApplicationDbContext context) 
        : IProjectManagementRepository
    {
        public async Task<List<ProjectDTo>> GetAllProjects()
        {
            // Using eager loading with `Include` for related `Children` and `Items`. 
            // The query is constructed with `AsQueryable` to allow deferred execution, 
            // ensuring data is not fetched until the query is actually executed, 
            var projects =  context.Projects
                .Include(x => x.Items)
                .Include(x => x.Children.Where(c => !c.IsDeleted))
                    .ThenInclude(x => x.Items.Where(c => !c.IsDeleted))
                .Where(x => !x.IsDeleted)
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
                .AsQueryable();

            if (!projects.Any())
            {
                return []; // just return an empty list.
            }
            var projectLookup = await projects.ToDictionaryAsync(x => x.Id);

            var finalResult = BuildProjectHierarchyFromList(projectLookup, projects);
            return finalResult;
        }

        /// <summary>
        /// Builds the project hierarchy using a Dictionary for O(1) parent-child lookups, 
        /// offering better performance than recursion for large datasets.
        /// </summary>
        /// <param name="projectLookup"></param>
        /// <param name="projects"></param>
        /// <returns></returns>
        private List<ProjectDTo> BuildProjectHierarchyFromList(Dictionary<Guid, ProjectDTo> projectLookup
            , IQueryable<ProjectDTo> projects)
        {
            List<ProjectDTo> rootProjects = [];

            foreach (var project in projects)
            {
                // Check if project has parentId and add it as a children to the ParentObject.
                if (project.ParentId.HasValue)
                {
                    if (projectLookup.TryGetValue(project.ParentId.Value, out var parentProject))
                    {
                        parentProject.Children.Add(project);
                    }
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
