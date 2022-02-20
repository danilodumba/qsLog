using System;
using System.Threading.Tasks;
using NSubstitute;
using qsLibPack.Repositories.Interfaces;
using qsLibPack.Validations;
using qsLibPack.Validations.Interface;
using qsLog.Applications.Models;
using qsLog.Applications.Services.Users;
using qsLog.Domains.Users;
using qsLog.Domains.Users.Repository;
using qsLog.Test.Applications.Models;
using qsLog.Test.Domain;
using Xunit;

namespace qsLog.Test.Applications.Services
{
    public class UserServiceTest
    {
        readonly IValidationService _validationService;
        readonly IUnitOfWork _uow;
        readonly IUserRepository _userRepository;

        public UserServiceTest()
        {
            _validationService = new ValidationService();
            _uow = Substitute.For<IUnitOfWork>();
            _userRepository = Substitute.For<IUserRepository>();
        }

        [Fact]
        public async Task Deve_Criar_Usuario_Valido()
        {
            var service = new UserService(_validationService, _uow, _userRepository);
            var model = UserModelTest.GetUser();
            await service.Create(model);

            await _userRepository.Received().CreateAsync(Arg.Any<User>());
            await _uow.Received().CommitAsync();
            Assert.True(_validationService.IsValid());
        }

        [Fact]
        public async Task Deve_Criar_Usuario_Com_Model_Ivalido()
        {
            var service = new UserService(_validationService, _uow, _userRepository);
            var model = new UserModel();
            await service.Create(model);

            await _userRepository.DidNotReceive().CreateAsync(Arg.Any<User>());
            await _uow.DidNotReceive().CommitAsync();
            Assert.False(_validationService.IsValid());
        }

        [Fact]
        public async Task Deve_Criar_Usuario_Com_Dominio_Invalido()
        {
            var service = new UserService(_validationService, _uow, _userRepository);
            var model = Substitute.For<UserModel>();
            model.IsValid().Returns(true);
            await service.Create(model);

            await _userRepository.DidNotReceive().CreateAsync(Arg.Any<User>());
            await _uow.DidNotReceive().CommitAsync();
            Assert.False(_validationService.IsValid());
        }

        [Fact]
        public async Task Deve_Criar_Usuario_Com_Login_Ja_Informado()
        {
            _userRepository.ExistsUserName("").ReturnsForAnyArgs(true);
            var service = new UserService(_validationService, _uow, _userRepository);
            var model = UserModelTest.GetUser();
            await service.Create(model);

            Assert.False(_validationService.IsValid());
            await _userRepository.DidNotReceive().CreateAsync(Arg.Any<User>());
            await _uow.DidNotReceive().CommitAsync();
        }

        [Fact]
        public async Task Deve_Alterar_Usuario_Valido()
        {
            _userRepository.GetByIDAsync(Guid.NewGuid()).ReturnsForAnyArgs(await Task.FromResult(UserTest.GetUser()));
            var service = new UserService(_validationService, _uow, _userRepository);
            var model = UserModelTest.GetUser();

            await service.Update(model, model.Id);

            Assert.True(_validationService.IsValid());
            await _userRepository.Received().UpdateAsync(Arg.Any<User>());
            await _uow.Received().CommitAsync();
        }

        [Fact]
        public async Task Deve_Resetar_Senha()
        {
            _userRepository.GetByIDAsync(Guid.NewGuid()).ReturnsForAnyArgs(await Task.FromResult(UserTest.GetUser()));
            var service = new UserService(_validationService, _uow, _userRepository);
            
            await service.ResetPassword(Guid.NewGuid());

            Assert.True(_validationService.IsValid());
            await _userRepository.Received().UpdateAsync(Arg.Any<User>());
            await _uow.Received().CommitAsync();
        }
    }
}