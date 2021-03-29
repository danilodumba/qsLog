using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using qsLogPack.Infrastructures.HttpQsLog;
using qsLogPack.Infrastructures.Interfaces;
using qsLogPack.Infrastructures.TXT;
using qsLogPack.Models;
using qsLogPack.Services;
using qsLogPack.Services.Interfaces;

namespace qsLogPack.IoC
{
    public static class LogIoC
    {
        public static void AddQsLog(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<QsLogSettings>(configuration.GetSection("QsLogSettings"));
            services.AddScoped<ILogRepository, LogHttpRepository>();
            services.AddScoped<ILogTxtRespository, LogTxtRepository>();
            services.AddScoped<ILogService, LogService>();
        }
    }
}