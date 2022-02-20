using Rebus.Handlers;
using System;
using qsLogPack.Models;
using System.Threading.Tasks;
using MediatR;
using qsLog.Applications.Commands;

namespace qsLog.Presentetion.EventHandlers
{
    public class CreateLogEventHandler : IHandleMessages<LogModel>
    {
        readonly IMediator _mediator;
        public CreateLogEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(LogModel message)
        {
            var command = new CreateLogCommand(message.Description, message.Source, (qsLog.Domains.Logs.LogTypeEnum)message.LogType, message.ApiKey);
            await _mediator.Send(command);
        }
    }
}