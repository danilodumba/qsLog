using System;
using qsLog.Domains.Logs;

namespace qsLog.Presentetion.Models
{
    public class LogModel
    {
        public string Description { get; set; }
        public string Source { get; set; }
        public LogTypeEnum LogType { get; set; }
        public Guid ProjectID { get; set; }
    }
}