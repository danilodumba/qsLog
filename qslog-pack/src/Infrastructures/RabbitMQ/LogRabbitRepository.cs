using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using qsLogPack.Infrastructures.Interfaces;
using qsLogPack.Models;
using Rebus.Bus;

namespace qsLogPack.Infrastructures.RabbitMQ
{
    public class LogRabbitRepository : ILogRepository
    {
        readonly IBus _bus;
        readonly QsLogSettings _logSettings;

        public LogRabbitRepository(
            IBus bus,
            IOptions<QsLogSettings> logSettings)
        {
            _bus = bus;
            _logSettings = logSettings.Value;
        }

        public async Task<Guid> Send(LogModel model)
        {
            model.ApiKey = Guid.Parse(_logSettings.ApiKey);
            await _bus.Publish(model);

            return model.Id;
        }
    }
}