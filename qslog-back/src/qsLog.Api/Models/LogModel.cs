using System;
using qsLog.Domains.Logs;

namespace qsLog.Presentetion.Models
{
    public class LogModel
    {
        public string Description { get; set; }
        public string Source { get; set; }
        public LogTypeEnum LogType { get; set; }
    }

    public class LogMessage
    {
        public Guid? Id { get; set; }
        public Guid? ApiKey { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public LogTypeEnum? LogType { get; set; }
        public DateTime? Date { get; set; }
    }
}