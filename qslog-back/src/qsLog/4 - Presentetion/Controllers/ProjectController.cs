using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using qsLog.Applications.Models;
using qsLog.Applications.Services.Interfaces;

namespace qsLog.Presentetion.Controllers
{
    public class ProjectController: ApiController
    {
        readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectModel model)
        {
            await _projectService.Create(model);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] ProjectModel model, Guid id)
        {
            await _projectService.Update(model, id);
            return NoContent();
        }

        [HttpGet]
        public IActionResult ListAll()
        {
            return Ok(_projectService.ListAll());
        }

        [HttpGet("{id}")]
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