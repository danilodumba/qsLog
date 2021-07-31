using FluentValidation;
using qsLog.Applications.Models;

namespace qsLog.Applications.Validations
{
    public class UserModelValidation : AbstractValidator<UserModel> 
    {
        public UserModelValidation()
        {
            this.RuleFor(x => x.Name).NotEmpty().WithMessage("Informe um nome");
            this.RuleFor(x => x.UserName).NotEmpty().WithMessage("Informe um usuário");
            this.RuleFor(x => x.Email).NotEmpty().WithMessage("Informe um e-mail")
                                      .EmailAddress().WithMessage("E-mail inválido");
        }
    }
}