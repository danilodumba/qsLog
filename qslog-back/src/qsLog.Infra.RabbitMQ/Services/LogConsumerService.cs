using System;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using qsLog.Applications.Commands;
using qsLog.Domain.Applications.Services.Interfaces;
using qsLog.Infrastructure.RabbitMQ.Messages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace qsLog.Infrastructure.RabbitMQ.Services
{
    public class LogConsumerService : IServiceBus
    {
        readonly IMediator _mediator;
        IConnection _connection;
        IModel _channel;
        readonly ILogger<LogConsumerService> _logger;
        public LogConsumerService(IMediator mediator, ILogger<LogConsumerService> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public Task Consumer()
        {
            this.CreateConnection();

            _channel.QueueDeclare(queue: "qslog",
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                _logger.LogInformation("Entrou no consumer.");
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                _logger.LogInformation(json);
                var message = JsonConvert.DeserializeObject<LogMessage>(json);
                if (message != null)
                    await CreateLog(message);

                 _logger.LogInformation("Finalizou consumer");
            };

            _channel.BasicConsume(queue: "qslog",
                                autoAck: true,
                                consumer: consumer);



            return Task.CompletedTask;
        }

        private async Task CreateLog(LogMessage message)
        {
            var command = new CreateLogCommand(message.Description, message.Source, (qsLog.Domains.Logs.LogTypeEnum)message.LogType, message.ApiKey);
            await _mediator.Send(command);
        }

        private void CreateConnection()
        {
            if (_connection != null && _connection.IsOpen)
                return;

            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_channel != null) _channel.Dispose();
            if (_connection != null) _connection.Dispose();
        }
    }
}