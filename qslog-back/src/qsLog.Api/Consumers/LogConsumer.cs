using System;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using qsLog.Applications.Commands;
using qsLog.Presentetion.Models;

namespace qsLog.Api.Consumers
{
    public class LogConsumer : IConsumer<LogMessage>
    {
        readonly IMediator _mediator;
        readonly ILogger<LogConsumer> _logger;
        public LogConsumer(IMediator mediator, ILogger<LogConsumer> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<LogMessage> context)
        {
           try
            {                   
                var log = context.Message;

                _logger.LogInformation($"Mensagem recebida. {log.Description}");

                var command = new CreateLogCommand(log.Description, log.Source, log.LogType.Value, log.ApiKey.Value);
                var id = await _mediator.Send(command);

                _logger.LogInformation($"Id do Log. {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao consumir o log", ex);
            }
        }
    }
}