using System.Text.Json.Serialization;

namespace project_management_v1.Application.DTOs
{
    public class ProjectDTo
    {
        public Guid Id { get; set; }

        //[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        // Always ignore this property in JSON serialization since it is not needed for this case.
        // Used only for querying purposes.
        public Guid? ParentId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ProjectItemDTo> Items { get; set; } = [];
        public List<ProjectDTo> Children { get; set; } = [];
    }
}
