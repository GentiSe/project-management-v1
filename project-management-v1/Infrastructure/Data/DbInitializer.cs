using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using project_management_v1.Application.Domain.Constants;
using project_management_v1.Application.Domain.Entities;
using project_management_v1.Application.Domain.Enums;
using project_management_v1.Infrastructure.Helpers;
using System.Data;
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
            // Commented code 
            //if (!context.Projects.Any())
            //{
            //    var parentProject = new Project
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "System Upgrade",
            //        Description = "Upgrade the entire corporate IT infrastructure.",
            //        CreatedAt = DateTime.UtcNow,
            //        IsDeleted = false
            //    };

            //    var childProject1 = new Project
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Server Migration",
            //        Description = "Migrate all legacy servers to cloud-based infrastructure.",
            //        CreatedAt = DateTime.UtcNow,
            //        IsDeleted = false,
            //        ParentId = parentProject.Id
            //    };

            //    var childProject2 = new Project
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Security Enhancement",
            //        Description = "security Enhancement",
            //        CreatedAt = DateTime.UtcNow,
            //        IsDeleted = false,
            //        ParentId = parentProject.Id
            //    };

            //    var subProject1 = new Project
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "SubProject Of Child 1",
            //        Description = "SubProject Of Child 1",
            //        CreatedAt = DateTime.UtcNow,
            //        IsDeleted = false,
            //        ParentId = childProject1.Id
            //    };

            //    await context.Projects
            //        .AddRangeAsync(parentProject, childProject1, childProject2, subProject1);

            //    var projectItems = GetProjectItems(childProject1.Id);
            //    await context.ProjectItems.AddRangeAsync(projectItems);
            //    await context.SaveChangesAsync();
            //}
            if (await roleManager.FindByNameAsync(UserRoles.Admin) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Basic));
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Analyst));
                await roleManager.CreateAsync(new IdentityRole(UserRoles.CategoryManager));
            }

            // Logic of crosscheck for both might not be valid if add another user here... or role above.
            // Added like this just for testing purposes since they are dummy data.
            if(await userManager.FindByEmailAsync("gentselimi7@gmail.com") == null)
            {
                await CreateUserWithRoleAsync("gentselimi7@gmail.com", "gentselimi7@gmail.com"
                , "Admin123.", UserRoles.Admin);

                await CreateUserWithRoleAsync("gentritSelimi", "gentselimi@gmail.com",
                    "Analyst123.", UserRoles.Analyst);
            }

            // Again the crosscheck might not be valid if add other projects/ items / or role acces for project.
            if(!await context.Projects.AnyAsync())
            {
                await SeedProjects();
            }
        }

        public async Task CreateUserWithRoleAsync(string username, string email, string password, string role)
        {
            var user = new IdentityUser
            {
                UserName = username,
                Email = email,
                EmailConfirmed = true,
                PhoneNumber = "044111222"
            };

            var res = await userManager.CreateAsync(user, password);

            if (res.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
                await userManager.AddClaimsAsync(user, new Claim[]
                {
                     new(ClaimTypes.Role, role)
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


        public async Task SeedProjects()
        {
            var retailProject = new Project
            {
                Id = Guid.NewGuid(),
                Name = "Retail",
                Description = "Retail sector projects focused on improving customer experience and sales.",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            var customerSatisfactionProject = new Project
            {
                Id = Guid.NewGuid(),
                Name = "Customer Satisfaction",
                Description = "Focus on improving customer experience and satisfaction.",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,
                ParentId = retailProject.Id
            };

            var stockProject = new Project
            {
                Id = Guid.NewGuid(),
                Name = "Stock",
                Description = "Manage product stock levels and optimize supply chain.",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,
                ParentId = retailProject.Id
            };

            var salesProject = new Project
            {
                Id = Guid.NewGuid(),
                Name = "Sales",
                Description = "Increase sales through marketing and customer engagement.",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,
                ParentId = retailProject.Id
            };

            var marketingProject = new Project
            {
                Id = Guid.NewGuid(),
                Name = "Marketing",
                Description = "Marketing campaigns and promotions to enhance brand visibility.",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            var promotionsProject = new Project
            {
                Id = Guid.NewGuid(),
                Name = "Promotions",
                Description = "Run promotional campaigns and advertisements.",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,
                ParentId = marketingProject.Id
            };

            await context.Projects.AddRangeAsync(retailProject, marketingProject,
                                      customerSatisfactionProject, stockProject, salesProject, promotionsProject);

            await context.ProjectItems.AddRangeAsync(
                new ProjectItem
                {
                    ProjectId = customerSatisfactionProject.Id,
                    Type = ProjectItemType.Task,
                    Title = "Survey Customers",
                    AssigneId = "user123",
                    ReporterId = "admin456",
                    Order = 1,
                    Time = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    Priority = ProjectItemPriority.High
                },
                new ProjectItem
                {
                    ProjectId = customerSatisfactionProject.Id,
                    Type = ProjectItemType.Note,
                    Text = "Analyze customer feedback"
                },
                new ProjectItem
                {
                    ProjectId = customerSatisfactionProject.Id,
                    Type = ProjectItemType.Attachment,
                    Url = "customer-feedback-form-url"
                },

                new ProjectItem
                {
                    ProjectId = stockProject.Id,
                    Type = ProjectItemType.Task,
                    Title = "Replenish Stock",
                    AssigneId = "user234",
                    ReporterId = "admin456",
                    Order = 1,
                    Time = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    Priority = ProjectItemPriority.Medium
                },
                new ProjectItem
                {
                    ProjectId = stockProject.Id,
                    Type = ProjectItemType.Note,
                    Text = "Check stock levels for top-selling items"
                },

                new ProjectItem
                {
                    ProjectId = salesProject.Id,
                    Type = ProjectItemType.Task,
                    Title = "Sales Strategy Planning",
                    AssigneId = "user345",
                    ReporterId = "admin456",
                    Order = 1,
                    Time = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    Priority = ProjectItemPriority.Low
                },
                new ProjectItem
                {
                    ProjectId = salesProject.Id,
                    Type = ProjectItemType.Note,
                    Text = "Discuss pricing and promotion strategies"
                }
            );

            await context.ProjectItems.AddRangeAsync(
                new ProjectItem
                {
                    ProjectId = promotionsProject.Id,
                    Type = ProjectItemType.Task,
                    Title = "Create Campaign",
                    AssigneId = "user456",
                    ReporterId = "admin789",
                    Order = 1,
                    Time = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    Priority = ProjectItemPriority.High
                },
                new ProjectItem
                {
                    ProjectId = promotionsProject.Id,
                    Type = ProjectItemType.Note,
                    Text = "Prepare marketing materials"
                },
                new ProjectItem
                {
                    ProjectId = promotionsProject.Id,
                    Type = ProjectItemType.Attachment,
                    Url = "campaign-assets-url"
                }
            );
            await context.SaveChangesAsync();

            var projects = new List<Project> { retailProject, marketingProject, customerSatisfactionProject, stockProject, salesProject, promotionsProject };
            await AssignDefaultRoles(projects);
        }

        private async Task AssignDefaultRoles(List<Project> projects)
        {
            var roles = new[] { UserRoles.Admin, UserRoles.Analyst, UserRoles.CategoryManager, UserRoles.Basic };
            var rolesInDb = await roleManager.Roles.ToListAsync();

            rolesInDb = rolesInDb.Where(x => roles.Contains(x.Name)).ToList();

            var list = new List<ProjectRoleAccess>();

            foreach (var project in projects)
            {
                foreach (var role in rolesInDb)
                {
                    if(role is not null && project is not null)
                    {
                        var item = new ProjectRoleAccess
                        {
                            ProjectId = project.Id,
                            RoleId = role.Id.ToLower(),
                            AccesType = ProjectHelpers.GetAccesTypeForRole(role.Name),
                            IsActive = true
                        };
                        list.Add(item);
                    }

                }
            }
            if(list.Count > 0)
            {
                await context.ProjectRoleAccesses.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
        }
    }
}
