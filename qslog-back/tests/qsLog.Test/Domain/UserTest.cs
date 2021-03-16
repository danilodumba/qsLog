using qsLibPack.Domain.Exceptions;
using qsLibPack.Domain.ValueObjects;
using qsLog.Domains.Users;
using Xunit;

namespace qsLog.Test.Domain
{
    public class UserTest
    {
        [Fact]
        public void Deve_Criar_Usuario_Com_Email_Invalido()
        {
            Assert.Throws<DomainException>(() => new User("Name", "UserName", "", new PasswordVO("1", "1"), false));
        }

        [Fact]
        public void Deve_Criar_Usuario_Com_Name_Invalido()
        {
            Assert.Throws<DomainException>(() => new User("", "UserName", "teste@teste.com", new PasswordVO("1", "1"), true));
        }

        [Fact]
        public void Deve_Criar_Usuario_Com_UserName_Invalido()
        {
            Assert.Throws<DomainException>(() => new User("Name", "", "teste@teste.com", new PasswordVO("1", "1"), true));
        }

        [Fact]
        public void Deve_Criar_Usuario_Com_Password_Invalido()
        {
            Assert.Throws<DomainException>(() => new User("Name", "UserName", "teste@teste.com", new PasswordVO("", ""), false));
        }

        [Fact]
        public void Deve_Criar_Usuario_Valido()
        {
            var user = GetUser();
            Assert.NotNull(user);
        }

        public static User GetUser()
        {
            return new User("danilo teste", "danilo", "teste@teste.com.br", new PasswordVO("123456", "123456"), true);
        }
    }
}