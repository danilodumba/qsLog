using qsLog.Applications.Models;
using Xunit;

namespace qsLog.Test.Applications.Models
{
    public class UserModelTest
    {
        [Theory]
        [InlineData("")]
        [InlineData("danilo@danilo")]
        public void Deve_Retornar_Email_Invalido(string email)
        {
            var userModel = GetUser();
            userModel.Email = email;

            Assert.False(userModel.IsValid());
        }

        [Theory]
        [InlineData("")]
        [InlineData("       ")]
        public void Deve_Retornar_Name_Invalido(string nome)
        {
            var userModel = GetUser();
            userModel.Name = nome;

            Assert.False(userModel.IsValid());
        }

        [Theory]
        [InlineData("")]
        [InlineData("       ")]
        public void Deve_Retornar_UserName_Invalido(string userName)
        {
            var userModel = GetUser();
            userModel.UserName = userName;

            Assert.False(userModel.IsValid());
        }


        [Fact]
        public void Deve_Retornar_User_Valido()
        {
            var userModel = GetUser();
            Assert.True(userModel.IsValid());
        }

        public static UserModel GetUser()
        {
            return new UserModel
            {
                Name = "teste nome",
                UserName = "teste.teste",
                Email = "teste@teste.com.br",
                Administrator = true
            };
        }
    }
}