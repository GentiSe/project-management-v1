using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using project_management_v1.Application.Domain.Entities;

namespace project_management_v1.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectItem> ProjectItems { get; set; }
        public DbSet<ProjectRoleAccess> ProjectRoleAccesses { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
