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
    [Route("api/[controller]")]
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
        public async Task<IActionResult> Create([FromBody] LogModel model)
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
                return NotFound("Log nao encontrado para o ID informado");

            return Ok(model);
        }

        [HttpGet("{firstDate}/{endDate}")]
        public IActionResult List(
            DateTime firstDate,
            DateTime endDate,
            string description,
            LogTypeEnum? type,
            Guid? projectID)
        {
            var period = new PeriodoVO(firstDate, endDate);
            return Ok(_logQueryRepository.List(period, description, projectID, type));
        }
    }
}