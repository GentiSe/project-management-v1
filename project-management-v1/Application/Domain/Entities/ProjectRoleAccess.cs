using Microsoft.AspNetCore.Identity;
using project_management_v1.Application.Domain.Enums;

namespace project_management_v1.Application.Domain.Entities
{
    public class ProjectRoleAccess
    {
        public int Id { get; set; }
        public Guid ProjectId { get; set; }
        public required string RoleId { get; set; }
        public AccesType AccesType { get; set; }
        public bool IsActive { get; set; }
        public virtual Project Project { get; set; }
        public virtual IdentityRole Role { get; set; }
    }
}
