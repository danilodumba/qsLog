using System;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using qsLibPack.Validations;
using qsLibPack.Validations.Interface;
using qsLog.Domain.Applications.Services.Interfaces;
using qsLog.Infrastructure.RabbitMQ.Services;

namespace qsLog.Infrastructure.RabbitMQ.IoC
{
    public static class ServiceCollectionExtension
    {
        public static void AddQsRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IServiceBus, LogConsumerService>();
        }
    }
}