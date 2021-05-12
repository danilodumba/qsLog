using System;
using qsLog.Applications.Core;
using qsLog.Applications.Validations;

namespace qsLog.Applications.Models
{
    public class ProjectModel: Model<Guid>
    {
        public string Name { get; set; }
        public Guid ApiKey { get; set; }

        public override bool IsValid()
        {
            var validator = new ProjectModelValidation();
            this.Errors = validator.Validate(this).Errors;

            return validator.Validate(this).IsValid;
        }
    }
}