using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using project_management_v1.Application.Domain.Entities;

namespace project_management_v1.Infrastructure.Data.EntityConfigurations
{
    public class ProjectEntityTypeConfigurations : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> b)
        {
            b.Property(x => x.Id)
                    .IsRequired();

            b.HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(b => b.ParentId)
                .OnDelete(DeleteBehavior.Cascade);

            b.Property(x => x.Name)
                .HasMaxLength(500)
                .IsRequired();

            b.Property(x => x.CreatedAt).IsRequired();

            b.Property(x => x.Description)
                .HasMaxLength(4000)
                .IsRequired(false);

            b.Property(x => x.ParentId).IsRequired(false);
            b.Property(x => x.IsDeleted).IsRequired();

            b.HasKey(x => x.Id);
            b.HasIndex(x => x.ParentId);

            b.ToTable("Projects");
        }
    }
}
