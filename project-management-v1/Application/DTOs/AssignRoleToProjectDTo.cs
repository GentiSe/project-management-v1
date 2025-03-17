using project_management_v1.Application.Domain.Enums;

namespace project_management_v1.Application.DTOs
{
    public class AssignRoleToProjectDTo
    {
        public Guid ProjectId { get; set; }
        public required string Role { get; set; }
    }
}
