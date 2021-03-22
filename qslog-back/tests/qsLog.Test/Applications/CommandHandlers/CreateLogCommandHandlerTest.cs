using System.Threading;
using System.Threading.Tasks;
using System;
using NSubstitute;
using qsLibPack.Repositories.Interfaces;
using qsLibPack.Validations;
using qsLibPack.Validations.Interface;
using qsLog.Applications.CommandHandlers;
using qsLog.Domains.Logs.Repository;
using qsLog.Domains.Projects.Repository;
using qsLog.Test.Applications.Commands;
using qsLog.Test.Domain;
using Xunit;
using qsLog.Domains.Logs;
using qsLog.Applications.Commands;

namespace qsLog.Test.Applications.CommandHandlers
{
    public class CreateLogCommandHandlerTest
    {
        readonly CreateLogCommandHandler _handler;
        readonly IValidationService _validationService = new ValidationService();
        readonly IUnitOfWork _uow = Substitute.For<IUnitOfWork>();
        readonly IProjectRepository _projectRepository = Substitute.For<IProjectRepository>();
        readonly ILogRepository _logRepository = Substitute.For<ILogRepository>();

        public CreateLogCommandHandlerTest()
        {
            _handler = new CreateLogCommandHandler(_validationService, _uow, _logRepository, _projectRepository);
        }

        [Fact]
        public async Task Deve_Incluir_Log_Valido()
        {
            var logCommand = CreateLogCommandTest.CommandValido();
            _projectRepository.GetByIDAsync(Arg.Any<Guid>()).Returns(await Task.FromResult(ProjectTest.GetProject()));

            var result = await _handler.Handle(logCommand, CancellationToken.None);

            Assert.True(result == Guid.Empty);
            await _logRepository.Received().CreateAsync(Arg.Any<Log>());
            await _uow.Received().CommitAsync();
        }

        [Fact]
        public async Task Deve_Validar_Log_Com_Command_Invalido()
        {
            var logCommand = CreateLogCommandTest.CommandValido();
            logCommand.Description = "";

            _projectRepository.GetByIDAsync(Arg.Any<Guid>()).Returns(await Task.FromResult(ProjectTest.GetProject()));

            var result = await _handler.Handle(logCommand, CancellationToken.None);

            Assert.False(_validationService.IsValid(), "O validation service deve ser invalido");
            Assert.True(result == Guid.Empty, "O ID do log deve retornar vazio.");
            await _logRepository.DidNotReceive().CreateAsync(Arg.Any<Log>());
            await _uow.DidNotReceive().CommitAsync();
        }

        [Fact]
        public async Task Deve_Validar_Log_Com_Project_Invalido()
        {
            var logCommand = CreateLogCommandTest.CommandValido();

            var result = await _handler.Handle(logCommand, CancellationToken.None);

            Assert.False(_validationService.IsValid(), "O validation service deve ser invalido");
            Assert.True(result == Guid.Empty, "O ID do log deve retornar vazio.");
            await _logRepository.DidNotReceive().CreateAsync(Arg.Any<Log>());
            await _uow.DidNotReceive().CommitAsync();
        }

        [Fact]
        public async Task Deve_Validar_Log_Com_Dominio_Invalido()
        {
            var logCommand = Substitute.For<CreateLogCommand>();
            logCommand.IsValid().Returns(true);

            var result = await _handler.Handle(logCommand, CancellationToken.None);

            Assert.False(_validationService.IsValid(), "O validation service deve ser invalido");
            Assert.True(result == Guid.Empty, "O ID do log deve retornar vazio.");
            await _logRepository.DidNotReceive().CreateAsync(Arg.Any<Log>());
            await _uow.DidNotReceive().CommitAsync();
        }
    }
}