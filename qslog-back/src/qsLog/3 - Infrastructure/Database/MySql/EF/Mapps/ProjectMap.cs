using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using qsLog.Domains.Projects;

namespace qsLog.Infrastructure.Database.MySql.EF.Mapps
{
    public class ProjectMap : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").HasColumnType("char(36)").IsRequired();
            builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
            builder.Property(x => x.ApiKey).HasColumnName("api-key").HasColumnType("char(36)").IsRequired();
           

            builder.ToTable("projects");
        }
    }
}