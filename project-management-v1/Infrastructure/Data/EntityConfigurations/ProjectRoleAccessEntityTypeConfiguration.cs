using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using project_management_v1.Application.Domain.Entities;

namespace project_management_v1.Infrastructure.Data.EntityConfigurations
{
    public class ProjectRoleAccessEntityTypeConfiguration : IEntityTypeConfiguration<ProjectRoleAccess>
    {
        public void Configure(EntityTypeBuilder<ProjectRoleAccess> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            builder.HasOne(x => x.Project)
                .WithMany(x => x.ProjectRoleAccesses)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            //builder.HasOne(x => x.Role).WithOne()
            //    .HasForeignKey<ProjectRoleAccess>(x => x.RoleId)
            //    .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.ProjectId)
                .IsRequired();

            builder.Property(x => x.RoleId)
                .IsRequired();

            builder.Property(x => x.AccesType)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.IsActive).IsRequired();

            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new {x.ProjectId, x.RoleId});

            builder.ToTable("ProjectRoleAccesses");

        }
    }
}
