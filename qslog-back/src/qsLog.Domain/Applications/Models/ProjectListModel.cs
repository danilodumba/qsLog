using System;

namespace qsLog.Applications.Models
{
    public class ProjectListModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ApiKey { get; set; }
    }
}