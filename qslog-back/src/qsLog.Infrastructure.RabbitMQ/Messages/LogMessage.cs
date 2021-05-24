using System;

namespace qsLog.Infrastructure.RabbitMQ.Messages
{
    public class LogMessage
    {
        public string Description { get; set; }
        public string Source { get; set; }
        public LogTypeEnum LogType { get; set; }
        public Guid Id { get; private set; }
        public Guid ApiKey { get; set; }    
        public DateTime Date {get; private set; }
    }

    public enum LogTypeEnum
    {
        Information = 1,
        Warning = 2,
        Error = 3
    }
}