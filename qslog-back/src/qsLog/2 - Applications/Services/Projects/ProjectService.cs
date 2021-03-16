using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using qsLibPack.Application;
using qsLibPack.Domain.Exceptions;
using qsLibPack.Repositories.Interfaces;
using qsLibPack.Validations.Interface;
using qsLog.Applications.Models;
using qsLog.Applications.Services.Interfaces;
using qsLog.Domains.Projects;
using qsLog.Domains.Projects.Repository;

namespace qsLog.Applications.Services.Projects
{
    public class ProjectService : ApplicationService, IProjectService
    {
        readonly IProjectRepository _projectRepository;
        public ProjectService(IValidationService validationService, IUnitOfWork uow, IProjectRepository projectRepository) : base(validationService, uow)
        {
            _projectRepository = projectRepository;
        }

        public async Task Create(ProjectModel model)
        {
            if (!model.IsValid())
            {
                _validationService.AddErrors(model.Errors);
                return;
            }

            try
            {
                var project = new Project(model.Name);
                await _projectRepository.CreateAsync(project);
                await _uow.CommitAsync();
            }
            catch (DomainException dx)
            {
                _validationService.AddErrors("P01", dx.Message);
            }
        }

        public IList<ProjectListModel> ListAll()
        {
            var projects = _projectRepository.ListAll();

            return projects.Select(p => new ProjectListModel()
            {
                Name = p.Name,
                ApiKey = p.ApiKey,
                Id = p.Id
            }).ToList();
        }

        public async Task<ProjectModel> GetByID(Guid id)
        {
            var project = await _projectRepository.GetByIDAsync(id);
            if (project == null)
            {
                _validationService.AddErrors("404", "Projeto não encontrado para o ID informado.");
                return new ProjectModel();
            }

            return new ProjectModel
            {
                Name = project.Name
            };
        }

        public Task<IList<ProjectModel>> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task Update(ProjectModel model, Guid id)
        {
            if (!model.IsValid())
            {
                _validationService.AddErrors(model.Errors);
                return;
            }

            try
            {
                var project = await _projectRepository.GetByIDAsync(id);
                if (project == null)
                {
                     _validationService.AddErrors("404", "Projeto não encontrado para o ID informado.");
                     return;
                }

                project.SetName(model.Name);
                _projectRepository.Update(project);
                _uow.Commit();
            }
            catch (DomainException dx)
            {
                _validationService.AddErrors("P01", dx.Message);
            }
        }
    }
}