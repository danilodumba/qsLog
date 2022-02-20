using System;
using qsLibPack.Domain.Entities;
using qsLibPack.Validations;
using qsLog.Domains.Projects;

namespace qsLog.Domains.Logs
{
    public class Log : AggregateRoot<Guid>
    {
        public Log(){}
        public Log(string description, string source, LogTypeEnum logType, Project project)
        {
            Description = description;
            Source = source;
            LogType = logType;
            Creation = DateTime.Now;
            Project = project;

            this.Validate();
        }

        public string Description { get; private set; }
        public string Source { get; private set; }
        public LogTypeEnum LogType { get; private set; }
        public DateTime Creation { get; private set; }
        public virtual Project Project { get; private set; }

        protected override void Validate()
        {
            this.Description.NotNullOrEmpty();
            this.Project.NotNull();
            this.LogType.NotNull();
        }
    }
}