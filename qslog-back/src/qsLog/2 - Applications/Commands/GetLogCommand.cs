using System;
using FluentValidation.Results;
using qsLibPack.Mediator;
using qsLog.Applications.Models;

namespace qsLog.Applications.Commands
{
    public class GetLogCommand : Command<LogModel>
    {
        public Guid Id { get; set; }
        public override bool IsValid()
        {
            if (this.Id == Guid.Empty)
            {
                this.Errors.Add(new ValidationFailure(nameof(this.Id), "Informe id valido para o log"));
                return false;
            }

            return true;
        }
    }
}