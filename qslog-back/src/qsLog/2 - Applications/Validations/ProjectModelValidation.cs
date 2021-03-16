using FluentValidation;
using qsLog.Applications.Models;

namespace qsLog.Applications.Validations
{
    public class ProjectModelValidation: AbstractValidator<ProjectModel> 
    {
        public ProjectModelValidation()
        {
            this.RuleFor(x => x.Name).NotEmpty().WithMessage("Informe um nome para o projeto");
        }
    }
}