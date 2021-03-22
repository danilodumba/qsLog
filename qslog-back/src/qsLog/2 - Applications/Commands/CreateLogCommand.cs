using System;
using qsLibPack.Mediator;
using qsLog.Applications.Validations;
using qsLog.Domains.Logs;

namespace qsLog.Applications.Commands
{
    public class CreateLogCommand : Command<Guid>
    {
        public CreateLogCommand() {}
        public CreateLogCommand(
            string description,
            string source,
            LogTypeEnum logType,
            Guid projectID)
        {
            Description = description;
            Source = source;
            LogType = logType;
            ProjectID = projectID;
        }

        public string Description { get; set; }
        public string Source { get; set; }
        public LogTypeEnum LogType { get; set; }
        public Guid ProjectID { get; set; }

        public override bool IsValid()
        {
            var validate = new LogCommandValidation();
            this.Errors = validate.Validate(this).Errors;

            return this.Errors.Count == 0;
        }
    }
}