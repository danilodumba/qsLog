using System;
using qsLibPack.Domain.Exceptions;
using qsLog.Domains.Projects;
using Xunit;

namespace qsLog.Test.Domain
{
    public class ProjectTest
    {
        [Fact]
        public void Deve_Criar_Projeto_Com_Nome_Invalido()
        {
            Assert.Throws<DomainException>(() => new Project(""));
        }

        [Fact]
        public void Deve_Criar_Projeto_Com_ApyKey()
        {
            var project = new Project("Project Test");
            Assert.True(project.ApiKey != Guid.Empty);
        }

        [Fact]
        public void Deve_Gerar_Nova_ApyKey()
        {
            var project = new Project("Project Test");
            var apiKey = project.ApiKey;
            project.GenerateApiKey();
            Assert.True(project.ApiKey != apiKey);
        }

        public static Project GetProject()
        {
            return new Project("Teste project");
        }
    }
}