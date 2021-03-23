using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using qsLibPack.Repositories.Interfaces;
using qsLog.Domains.Logs.Repository;
using qsLog.Domains.Projects.Repository;
using qsLog.Domains.Users.Repository;
using qsLog.Infrastructure.Database.MySql.EF.Contexts;
using qsLog.Infrastructure.Database.MySql.EF.Core;
using qsLog.Infrastructure.Database.MySql.EF.QueryRepositories;
using qsLog.Infrastructure.Database.MySql.EF.Repository;

namespace qsLog.Infrastructure.Database.MySql.EF.IoC
{
    public static class DatabaseServiceCollectionExtensions
    {
        public static void AddInfraDatabaseMySql(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<LogContext>(options => options.UseMySQL(connectionString));
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<ILogQueryRepository, LogQueryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}