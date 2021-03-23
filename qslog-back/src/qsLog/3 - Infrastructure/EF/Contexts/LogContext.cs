using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using qsLog.Domains.Logs;
using qsLog.Domains.Projects;
using qsLog.Domains.Users;

namespace qsLog.Infrastructure.EF.Contexts
{
    public class LogContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        
        public LogContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}