using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using project_management_v1.Application.Domain.Entities;

namespace project_management_v1.Infrastructure.Data.EntityConfigurations
{
    public class ProjectItemEntityTypeConfiguration : IEntityTypeConfiguration<ProjectItem>
    {
        public void Configure(EntityTypeBuilder<ProjectItem> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            builder.Property(p => p.ProjectId)
                   .IsRequired();

            builder.Property(p => p.Type)
                   .IsRequired();

            builder.Property(p => p.Title).IsRequired(false)
                   .HasMaxLength(255);

            builder.Property(p => p.AssigneId)
                   .IsRequired(false)
                   .HasMaxLength(100);

            builder.Property(p => p.ReporterId).IsRequired(false)
                   .HasMaxLength(100);

            builder.Property(p => p.Order).IsRequired(false)
                .IsRequired(false);

            builder.Property(p => p.Time).IsRequired(false);

            builder.Property(p => p.Priority).IsRequired(false)
                   .HasConversion<string>();

            builder.Property(p => p.Text).IsRequired(false)
                   .HasColumnType("TEXT");

            builder.Property(p => p.Url).IsRequired(false)
                   .HasMaxLength(500);

            builder.Property(x => x.IsDeleted)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.ProjectId);
            builder.HasIndex(p => p.Type);

            builder.ToTable("ProjectItems");
        }
    }
}
