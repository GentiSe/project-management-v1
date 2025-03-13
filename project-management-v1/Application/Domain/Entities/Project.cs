namespace project_management_v1.Application.Domain.Entities
{
    /// <summary>
    /// Utilizes a self-referencing table to store projects and their children (unlimited layers of hierarchy). 
    /// </summary>
    public class Project
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public bool IsDeleted { get; set; }
        public Guid? ParentId { get; set; }
        public virtual Project? Parent { get; set; }
        public virtual ICollection<Project> Children { get; set; } = [];
        public virtual ICollection<ProjectItem> Items { get; set; } = [];
    }
}
