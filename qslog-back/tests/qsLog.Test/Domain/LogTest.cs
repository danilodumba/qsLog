using System;
using qsLibPack.Domain.Exceptions;
using qsLog.Domains.Logs;
using qsLog.Domains.Projects;
using Xunit;

namespace qsLog.Test.Domain
{
    public class LogTest
    {
        [Fact]
        public void Deve_Criar_Log_Com_Description_Invalido()
        {
            Assert.Throws<DomainException>(() => new Log("", "Source", LogTypeEnum.Error, GetProject()));
        }

        [Fact]
        public void Deve_Criar_Log_Com_Project_Invalido()
        {
            Assert.Throws<DomainException>(() => new Log("Nome", "Source", LogTypeEnum.Information, null));
        }

        [Fact]
        public void Deve_Validar_Data_Criacao()
        {
            var log = new Log("Nome", "Source", LogTypeEnum.Information, GetProject());
            Assert.True(log.Creation > DateTime.MinValue);
        }

        private static Project GetProject()
        {
            return new Project("Project Test");
        }
    }
}