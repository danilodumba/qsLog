using qsLogPack.Infrastructures.TXT;
using Xunit;
using NSubstitute;
using Microsoft.Extensions.Options;
using qsLogPack.Models;
using System;

namespace qsLogPack.Test.Repositories.TXT
{
    public class LogTxtRepositoryTest
    {
        [Fact]
        public void Deve_Criar_LogTxtString()
        {
            var settings = Substitute.For<IOptions<QsLogSettings>>();
            settings.Value.Returns(new QsLogSettings());

            var repository = new LogTxtRepository(settings);

            repository.Create("Teste de sistema simples.");
        }

        [Fact]
        public void Deve_Criar_LogTxtException()
        {
            var settings = Substitute.For<IOptions<QsLogSettings>>();
            settings.Value.Returns(new QsLogSettings());

            var repository = new LogTxtRepository(settings);

            repository.Create(new Exception("Erro no sistema XPTO"));
        }
    }
}