using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using qsLibPack.Domain.ValueObjects.Br;
using qsLog.Applications.Commands;
using qsLog.Domains.Logs;
using qsLog.Domains.Logs.Repository;
using qsLog.Domains.Projects.Repository;
using qsLog.Presentetion.Attributes;
using qsLog.Presentetion.Models;

namespace qsLog.Presentetion.Controllers
{
    [Route("api/[controller]")]
    public class LogController: ApiController
    {
        readonly IMediator _mediator;
        readonly ILogQueryRepository _logQueryRepository;
        readonly IProjectRepository _projectRepository;
        public LogController(IMediator projectService,
                             ILogQueryRepository logQueryRepository, IProjectRepository projectRepository)
        {
            _mediator = projectService;
            _logQueryRepository = logQueryRepository;
            _projectRepository = projectRepository;
        }

        [HttpPost]
        [LogApiKey]
        public async Task<IActionResult> Create([FromBody] LogModel model)
        {
            var command = new CreateLogCommand(model.Description, model.Source, model.LogType, this.GetProjectApiKey());
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

        private Guid GetProjectApiKey()
        {
            if (this.HttpContext.Request.Query.TryGetValue("api-key", out var apiKeyQuery))
            {
                if(Guid.TryParse(apiKeyQuery, out var apiKey))
                {
                    return apiKey;
                }
            }
            
            return Guid.Empty;
        }
    }
}