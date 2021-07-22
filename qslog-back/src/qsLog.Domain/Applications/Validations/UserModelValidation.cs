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
            // this.RuleFor(x => x.Password).NotEmpty().WithMessage("Informe uma senha");
            // this.RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Confirme sua senha")
            //                                     .Equal(x => x.Password).WithMessage("Senhas não conferem");
        }
    }
}