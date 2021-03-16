using System;
using qsLibPack.Repositories.Interfaces;
using qsLibPack.Validations;
using qsLibPack.Validations.Interface;
using qsLog.Domains.Projects.Repository;
using NSubstitute;
using qsLog.Applications.Services.Projects;
using qsLog.Test.Applications.Models;
using System.Threading.Tasks;
using Xunit;
using qsLog.Domains.Projects;
using qsLog.Applications.Models;
using qsLog.Test.Domain;

namespace qsLog.Test.Applications.Services
{
    public class ProjectServiceTest
    {
        readonly IValidationService _validationService;
        readonly IUnitOfWork _uow;
        readonly IProjectRepository _projectRepository;

        public ProjectServiceTest()
        {
            _validationService = new ValidationService();
            _uow = Substitute.For<IUnitOfWork>();
            _projectRepository = Substitute.For<IProjectRepository>();
        }

        [Fact]
        public async Task Deve_Criar_Projeto_Valido()
        {
            var service = new ProjectService(_validationService, _uow, _projectRepository);
            var project = ProjectModelTest.GetProject();
            await service.Create(project);

            await _projectRepository.Received().CreateAsync(Arg.Any<Project>());
            await _uow.Received().CommitAsync();
            Assert.True(_validationService.IsValid());
        }

        [Fact]
        public async Task Deve_Criar_Projeto_Com_Model_Invalido()
        {
            var service = new ProjectService(_validationService, _uow, _projectRepository);
            var model = new ProjectModel();
            await service.Create(model);

            Assert.False(_validationService.IsValid());
            await _projectRepository.DidNotReceive().CreateAsync(Arg.Any<Project>());
            await _uow.DidNotReceive().CommitAsync();
        }

        [Fact]
        public async Task Deve_Criar_Projeto_Com_Dominio_Invalido()
        {
            var service = new ProjectService(_validationService, _uow, _projectRepository);
            var model = Substitute.For<ProjectModel>();
            model.IsValid().ReturnsForAnyArgs(true);
            await service.Create(model);

            Assert.False(_validationService.IsValid());
            await _projectRepository.DidNotReceive().CreateAsync(Arg.Any<Project>());
            await _uow.DidNotReceive().CommitAsync();
        }

        [Fact]
        public async Task Deve_Alterar_Projeto_Valido()
        {
            _projectRepository.GetByIDAsync(Arg.Any<Guid>()).Returns(await Task.FromResult(ProjectTest.GetProject()));
            var service = new ProjectService(_validationService, _uow, _projectRepository);
            var project = ProjectModelTest.GetProject();
            await service.Update(project, Guid.NewGuid());

            _projectRepository.Received().Update(Arg.Any<Project>());
            _uow.Received().Commit();
            Assert.True(_validationService.IsValid());
        }

        [Fact]
        public async Task Deve_Alterar_Projeto_Com_Model_Invalido()
        {
            var service = new ProjectService(_validationService, _uow, _projectRepository);
            var model = new ProjectModel();
            await service.Update(model, Guid.NewGuid());

            _projectRepository.DidNotReceive().Update(Arg.Any<Project>());
            _uow.DidNotReceive().Commit();
            Assert.False(_validationService.IsValid());
        }

        [Fact]
        public async Task Deve_Alterar_Projeto_Com_Dominio_Invalido()
        {
             _projectRepository.GetByIDAsync(Arg.Any<Guid>()).Returns(await Task.FromResult(ProjectTest.GetProject()));
            var service = new ProjectService(_validationService, _uow, _projectRepository);
            var model = Substitute.For<ProjectModel>();
            model.IsValid().ReturnsForAnyArgs(true);
            await service.Update(model, Guid.NewGuid());

            Assert.False(_validationService.IsValid());
            _projectRepository.DidNotReceive().Update(Arg.Any<Project>());
            _uow.DidNotReceive().Commit();
        }

        [Fact]
        public async Task Deve_Alterar_Projeto_Com_Projeto_Nao_Encontrado()
        {
            Project project = null;
             _projectRepository.GetByIDAsync(Arg.Any<Guid>()).Returns(await Task.FromResult(project));
            var service = new ProjectService(_validationService, _uow, _projectRepository);
            var model = Substitute.For<ProjectModel>();
            model.IsValid().ReturnsForAnyArgs(true);
            await service.Update(model, Guid.NewGuid());

            Assert.False(_validationService.IsValid());
            _projectRepository.DidNotReceive().Update(Arg.Any<Project>());
            _uow.DidNotReceive().Commit();
        }
    }
}