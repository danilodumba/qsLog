using System;
using qsLog.Domains.Logs;

namespace qsLog.Applications.Models
{
    public class GetLogCommandOutput
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public LogTypeEnum LogType { get; set; }
        public Guid ProjectID { get; set; }
    }
}