using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using qsLog.Api.Models;
using qsLog.Applications.Models;
using qsLog.Applications.Services.Interfaces;

namespace qsLog.Presentetion.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    public class ProjectController: ApiController
    {
        readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] CreateProjectModel model)
        {
            var project = new ProjectModel
            {
                Name = model.Name
            };

            var id = await _projectService.Create(project);
            if (id == Guid.Empty)
                return NoContent();
            
            return CreatedAtRoute("Get", new {id}, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] CreateProjectModel model, Guid id)
        {
            var project = new ProjectModel
            {
                Name = model.Name
            };

            await _projectService.Update(project, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            await _projectService.Remove(id);
            return NoContent();
        }

        [HttpGet]
        public IActionResult ListByName(string name)
        {
            return Ok(_projectService.ListByName(name));
        }

        [HttpGet("{id}", Name="Get")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var model = await _projectService.GetByID(id);
            if (model.IsValid())
            {
                return Ok(model);
            }

            return NotFound("Projeto não encontrado");
        }
    }
}