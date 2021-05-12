using System;
using qsLog.Applications.Core;
using qsLog.Applications.Validations;

namespace qsLog.Applications.Models
{
    public class UserModel: Model<Guid>
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool Administrator {get; set; }

        public override bool IsValid()
        {
            var validator = new UserModelValidation();
            this.Errors = validator.Validate(this).Errors;

            return validator.Validate(this).IsValid;
        }
    }

    public class UserListModel
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Guid Id { get; set; }
        public bool Administrator {get; set; }
    }
}