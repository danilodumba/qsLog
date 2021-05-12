using System;

namespace qsLog.Domains.Logs.DTO
{
    public class LogListDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int LogType { get; set; }
        public DateTime Creation { get; set; }
        public string Project { get; set; }
    }
}