using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using qsLog.Domain.Applications.Services.Interfaces;

namespace qsLog.Api.Services
{
    public class ConsumerService : BackgroundService
    {
        private readonly ILogger<ConsumerService> _logger;
        IServiceBus _serviceBus;
        readonly IServiceProvider _services;
        IServiceScope _scope;
        public ConsumerService(ILogger<ConsumerService> logger, IServiceProvider services)
        {
            _logger = logger;
            // _serviceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus), "Servico nao inicializado");
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Host de mensageria iniciado.");

            try
            {
                _scope = _services.CreateScope();
                _serviceBus = _scope.ServiceProvider.GetRequiredService<IServiceBus>();

                await _serviceBus.Consumer();

            }
            catch (Exception ox)
            {
                _logger.LogError(ox.Message);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Host de mensageria finalizado.");

            if (_serviceBus != null)
            {
                _serviceBus.Dispose();
                _scope.Dispose();
            }

            await base.StopAsync(stoppingToken);
        }
    }
}