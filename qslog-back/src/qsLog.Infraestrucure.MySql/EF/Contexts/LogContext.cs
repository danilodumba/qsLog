using Microsoft.EntityFrameworkCore;
using qsLog.Domains.Logs;
using qsLog.Domains.Projects;
using qsLog.Domains.Users;
using qsLog.Infrastructure.Database.MySql.EF.Mapps;

namespace qsLog.Infrastructure.Database.MySql.EF.Contexts
{
    public class LogContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        
        public LogContext(DbContextOptions<LogContext> options)
            : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new ProjectMap());
            modelBuilder.ApplyConfiguration(new LogMap());
        }
    }
}