using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using qsLog.Domains.Logs;

namespace qsLog.Infrastructure.Database.MySql.EF.Mapps
{
    public class LogMap : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").HasColumnType("char(36)").IsRequired();
            builder.Property(x => x.Description).HasColumnName("description").IsRequired();
            builder.Property(x => x.Source).HasColumnName("source");
            builder.Property(x => x.Creation).HasColumnName("creation").IsRequired();
            builder.Property(x => x.LogType).HasColumnName("log_type").HasColumnType("smallint").IsRequired();
           
            builder.HasOne(x => x.Project)
                .WithMany()
                .HasForeignKey("project_id");

            builder.ToTable("logs");
        }
    }
}