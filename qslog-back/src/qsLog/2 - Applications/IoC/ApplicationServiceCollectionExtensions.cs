using MediatR;
using Microsoft.Extensions.DependencyInjection;
using qsLog.Applications.Services.Interfaces;
using qsLog.Applications.Services.Projects;
using qsLog.Applications.Services.Users;

namespace qsLog.Applications.IoC
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, System.Type type)
        {
            services.AddMediatR(type);

            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}