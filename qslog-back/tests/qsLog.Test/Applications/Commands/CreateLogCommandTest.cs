using System;
using qsLog.Applications.Commands;
using qsLog.Domains.Logs;
using Xunit;

namespace qsLog.Test.Applications.Commands
{
    public class CreateLogCommandTest
    {
        [Fact]
        public void Deve_Criar_Log_Valido()
        {
            var command = CommandValido();

            Assert.True(command.IsValid());
        }

        [Fact]
        public void Deve_Criar_Log_Com_Description_Invalido()
        {
            var command = CommandValido();
            command.Description = "    ";

            Assert.False(command.IsValid());
        }

        [Fact]
        public void Deve_Criar_Log_Com_ApiKey_Invalido()
        {
            var command = CommandValido();
            command.ApiKey = Guid.Empty;

            Assert.False(command.IsValid());
        }

        [Fact]
        public void Deve_Criar_Log_Com_Type_Invalido()
        {
            var command = CommandValido();
            command.LogType = (LogTypeEnum)10;

            Assert.False(command.IsValid());
        }

        public static CreateLogCommand CommandValido()
        {
            return new CreateLogCommand("Erro de teste ao acessar teste", "", Domains.Logs.LogTypeEnum.Error, Guid.NewGuid());
        }
    }
}