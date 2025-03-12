using project_management_v1.Domain.Enums;

namespace project_management_v1.Domain.Entities
{
    public class ProjectItem
    {
        public long Id { get; set; }
        public Guid ProjectId { get; set; }
        public ProjectItemType Type { get; set; }
        public string? Title { get; set; }
        public string? AssigneId { get; set; }
        public string? ReporterId { get; set; }
        public long? Order { get; set; }
        public long? Time { get; set; }
        public ProjectItemPriority? Priority { get; set; }
        public string? Text { get; set; }
        public string? Url { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual Project Project { get; set; }
    }
}
