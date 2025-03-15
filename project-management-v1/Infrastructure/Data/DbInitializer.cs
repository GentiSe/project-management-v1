using Microsoft.AspNetCore.Identity;
using project_management_v1.Application.Domain.Constants;
using project_management_v1.Application.Domain.Entities;
using project_management_v1.Application.Domain.Enums;
using System.Security.Claims;

namespace project_management_v1.Infrastructure.Data
{
    public interface IDbInitializer
    {
        Task Initialize();
    }
    public class DbInitializer(ApplicationDbContext context, RoleManager<IdentityRole> roleManager,
        UserManager<IdentityUser> userManager) 
        : IDbInitializer
    {
        // Added some dummy data for testing purposes.
        public async Task Initialize()
        {
            if (!context.Projects.Any())
            {

                var parentProject = new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "System Upgrade",
                    Description = "Upgrade the entire corporate IT infrastructure.",
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                };

                var childProject1 = new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "Server Migration",
                    Description = "Migrate all legacy servers to cloud-based infrastructure.",
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false,
                    ParentId = parentProject.Id
                };

                var childProject2 = new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "Security Enhancement",
                    Description = "security Enhancement",
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false,
                    ParentId = parentProject.Id
                };

                var subProject1 = new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "SubProject Of Child 1",
                    Description = "SubProject Of Child 1",
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false,
                    ParentId = childProject1.Id
                };

                await context.Projects
                    .AddRangeAsync(parentProject, childProject1, childProject2, subProject1);

                var projectItems = GetProjectItems(childProject1.Id);
                await context.ProjectItems.AddRangeAsync(projectItems);
                await context.SaveChangesAsync();
            }
            var t = await roleManager.FindByNameAsync(UserRoles.Admin);
            if (await roleManager.FindByNameAsync(UserRoles.Admin) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Basic));
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Analyst));

            }
            var adminUser = new IdentityUser
            {
                UserName = "gentselimi7@gmail.com",
                Email = "gentselimi7@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "044352799",
            };

            var res = await userManager.CreateAsync(adminUser, "Admin123.");

            if (res.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, UserRoles.Admin);

                await userManager.AddClaimsAsync(adminUser, new Claim[]
                {
                new(ClaimTypes.Role, UserRoles.Admin)
                });
            }

        }

        private List<ProjectItem> GetProjectItems(Guid childProjectId1)
        {
            var projectItems = new List<ProjectItem>
        {
                new() {
                    ProjectId = childProjectId1,
                    Type = ProjectItemType.Task,
                    Title = "setup project",
                    AssigneId = "user-gent",
                    ReporterId = "genti",
                    Order = 1,
                    Time = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    Priority = ProjectItemPriority.High
                },
                new() {
                    ProjectId = childProjectId1,
                    Type = ProjectItemType.Task,
                    Title = "Migrate Database",
                    AssigneId = "user789",
                    ReporterId = "admin456",
                    Order = 2,
                    Time = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    Priority = ProjectItemPriority.Medium
                },

                new() {
                    ProjectId = childProjectId1,
                    Type = ProjectItemType.Note,
                    Text = "migration note"
                },
                new() {
                    ProjectId = childProjectId1,
                    Type = ProjectItemType.Note,
                    Text = "Notify stakeholders"
                },
                new() {
                    ProjectId = childProjectId1,
                    Type = ProjectItemType.Attachment,
                    Url = "url-test"
                }
            };

            return projectItems;
        }
    }
}
