using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using qsLog.Domains.Users;

namespace qsLog.Infrastructure.Database.MySql.EF.Mapps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").HasColumnType("char(36)").IsRequired();
            builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
            builder.Property(x => x.Email).HasColumnName("email").HasMaxLength(250).IsRequired();
            builder.Property(x => x.Password).HasColumnName("password").HasMaxLength(20).IsRequired();
            builder.Property(x => x.Administrator).HasColumnName("administrator").HasColumnType("smallint").IsRequired();
            builder.Property(x => x.UserName).HasColumnName("user_name").HasMaxLength(30).IsRequired();

            builder.ToTable("users");
        }
    }
}