using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using qsLibPack.Repositories.Mongo.IoC;
using qsLog.Domains.Logs.Repository;
using qsLog.Domains.Projects;
using qsLog.Domains.Projects.Repository;
using qsLog.Domains.Users.Repository;
using qsLog.Infrastructure.Database.MongoDB.QueryRepository;
using qsLog.Infrastructure.Database.MongoDB.Repositories;

namespace qsLog.Infrastructure.Database.MongoDB.IoC
{
    public static class DatabaseMongoDBServiceCollectionExtensions
    {
        public static void AddInfraDatabaseMongoDB(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddQsLibPackMongo(configuration);

            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<ILogQueryRepository, LogQueryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
        }
    }
}