using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using qsLibPack.Domain.ValueObjects.Br;
using qsLog.Applications.Commands;
using qsLog.Domains.Logs;
using qsLog.Domains.Logs.Repository;
using qsLog.Presentetion.Models;

namespace qsLog.Presentetion.Controllers
{
    public class LogController: ApiController
    {
        readonly IMediator _mediator;
        readonly ILogQueryRepository _logQueryRepository;
        public LogController(IMediator projectService,
                             ILogQueryRepository logQueryRepository)
        {
            _mediator = projectService;
            _logQueryRepository = logQueryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(LogModel model)
        {
            var command = new CreateLogCommand(model.Description, model.Source, model.LogType, model.ProjectID);
            var logID = await _mediator.Send(command);
            if (logID == Guid.Empty) 
                return NoContent();

            return Ok(logID);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetLogCommand
            {
                Id = id
            };

            var model = await _mediator.Send(command);
            if (model.Id == Guid.Empty) 
                return NoContent();

            return Ok(model);
        }

        [HttpGet("{dataInicial}/{dataFinal}")]
        public IActionResult List(
            DateTime dataInicial,
            DateTime dataFinal,
            string nome,
            LogTypeEnum? type,
            Guid? projectID)
        {
            var periodo = new PeriodoVO(dataInicial, dataFinal);
            return Ok(_logQueryRepository.List(periodo, nome, projectID, type));
        }
    }
}