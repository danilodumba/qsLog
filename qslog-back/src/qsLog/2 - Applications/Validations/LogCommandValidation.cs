using FluentValidation;
using qsLog.Applications.Commands;
using qsLog.Domains.Logs;

namespace qsLog.Applications.Validations
{
    public class LogCommandValidation: AbstractValidator<CreateLogCommand> 
    {
        public LogCommandValidation()
        {
            this.RuleFor(x => x.Description).NotEmpty().WithMessage("Informe uma descricao para o log");
            this.RuleFor(x => x.ProjectID).NotEmpty().NotNull().WithMessage("Informe um projeto para o log");
            this.RuleFor(x => x.LogType).IsInEnum().WithMessage("Informe um tipo valido para o erro.");
        }
    }
}