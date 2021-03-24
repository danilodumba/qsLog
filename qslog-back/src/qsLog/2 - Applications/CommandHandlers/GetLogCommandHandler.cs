using System.Threading;
using System.Threading.Tasks;
using MediatR;
using qsLibPack.Validations.Interface;
using qsLog.Applications.Commands;
using qsLog.Applications.Models;
using qsLog.Domains.Logs.Repository;

namespace qsLog.Applications.CommandHandlers
{
    public class GetLogCommandHandler : IRequestHandler<GetLogCommand, GetLogCommandOutput>
    {
        readonly ILogRepository _logRepository;
        readonly IValidationService _validationService;

        public GetLogCommandHandler(ILogRepository logRepository, IValidationService validationService)
        {
            _logRepository = logRepository;
            _validationService = validationService;
        }

        public async Task<GetLogCommandOutput> Handle(GetLogCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                _validationService.AddErrors(request.Errors);
                 return new GetLogCommandOutput();
            }

            var log = await _logRepository.GetByIDAsync(request.Id);
            if (log == null)
            {
                 _validationService.AddErrors("01", "Log nao encontrado");
                 return new GetLogCommandOutput();
            }

            return new GetLogCommandOutput
            {
                Description = log.Description,
                Source = log.Source,
                LogType = log.LogType,
                ProjectID = log.Project.Id,
                Id = log.Id
            };
        }
    }
}