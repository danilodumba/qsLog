using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using qsLogPack.Infrastructures.HttpQsLog;
using qsLogPack.Infrastructures.Interfaces;
using qsLogPack.Infrastructures.RabbitMQ;
using qsLogPack.Infrastructures.TXT;
using qsLogPack.Models;
using qsLogPack.Services;
using qsLogPack.Services.Interfaces;
using Rebus.Config;
using Rebus.ServiceProvider;

namespace qsLogPack.IoC
{
    public static class LogIoC
    {
        public static void AddQsLog(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<QsLogSettings>(configuration.GetSection("QsLogSettings"));
            
            services.AddScoped<ILogTxtRespository, LogTxtRepository>();
            services.AddScoped<ILogService, LogService>();

            var settings = configuration.GetSection("QsLogSettings");
            var useHabbitMQ = settings.GetValue<bool?>("UseHabbitMQ") ?? false;
            if (useHabbitMQ)
            {
                var queue = settings.GetValue<string>("Queue");
                var connectionRabbit = settings.GetValue<string>("RabbitConnection");

                services.AddScoped<ILogRepository, LogRabbitRepository>();

                services.AddRebus(c => c
                    .Transport(t => t.UseRabbitMq(connectionRabbit, queue))
                );
            }
            else 
            {
                services.AddScoped<ILogRepository, LogHttpRepository>();
            }
        }
    }
}