using System;
using System.Threading;
using System.Threading.Tasks;
using qsLibPack.Domain.Exceptions;
using qsLibPack.Mediator;
using qsLibPack.Repositories.Interfaces;
using qsLibPack.Validations.Interface;
using qsLog.Applications.Commands;
using qsLog.Domains.Logs;
using qsLog.Domains.Logs.Repository;
using qsLog.Domains.Projects.Repository;

namespace qsLog.Applications.CommandHandlers
{
    public class CreateLogCommandHandler : CommandHandler<CreateLogCommand, Guid>
    {
        readonly ILogRepository _logRepository;
        readonly IProjectRepository _projectRepository;
        public CreateLogCommandHandler(
            IValidationService validationService,
            IUnitOfWork uow,
            ILogRepository logRepository,
            IProjectRepository projectRepository) : base(validationService, uow)
        {
            _logRepository = logRepository;
            _projectRepository = projectRepository;
        }

        public override async Task<Guid> Handle(CreateLogCommand request, CancellationToken cancellationToken)
        {
            if (!this.IsValidCommand(request))
            {
                return await Task.FromResult(Guid.Empty);
            }

            try
            {
                var projects = _projectRepository.ListAll();
                var project = _projectRepository.GetByApiKey(request.ApiKey);
                if (project == null)
                {
                    _validationService.AddErrors("01", "Projeto nao encontrado para a api-key informada.");
                    return await Task.FromResult(Guid.Empty);
                }

                var log = new Log(request.Description, request.Source, request.LogType, project);

                await _logRepository.CreateAsync(log);
                await _uow.CommitAsync();

                return log.Id;
            }
            catch (DomainException ex)
            {
                _validationService.AddErrors("02", ex.Message);
                return await Task.FromResult(Guid.Empty);
            }
        }
    }
}