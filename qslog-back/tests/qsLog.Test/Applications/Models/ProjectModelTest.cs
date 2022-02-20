using qsLog.Applications.Models;
using Xunit;

namespace qsLog.Test.Applications.Models
{
    public class ProjectModelTest
    {
        [Theory]
        [InlineData("")]
        [InlineData("       ")]
        public void Deve_Retornar_Name_Invalido(string nome)
        {
            var model = GetProject();
            model.Name = nome;

            Assert.False(model.IsValid());
        }

        [Fact]
        public void Deve_Retornar_Project_Valido()
        {
            var model = GetProject();
            Assert.True(model.IsValid());
        }

        public static ProjectModel GetProject()
        {
            return new ProjectModel
            {
                Name = "teste nome"
            };
        }
    }
}