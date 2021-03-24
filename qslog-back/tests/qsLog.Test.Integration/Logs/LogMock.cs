using System;
using System.Threading.Tasks;
using Bogus;
using qsLog.Domains.Logs;
using qsLog.Presentetion.Models;
using qsLog.Test.Integration.Projects;
using Xunit;

namespace qsLog.Test.Integration.Logs
{
    public static class LogMock
    {
        public static LogModel GetLogModel()
        {
            var faker = new Faker<LogModel>("pt_BR")
                .RuleFor(u => u.Description, f => f.Lorem.Sentence(50))
                // .RuleFor(u => u.Source, f => f.Lorem.Sentence(2000))
                .RuleFor(u => u.Source, f => f.System.Exception().ToString())
                .RuleFor(u => u.LogType, f => (LogTypeEnum)f.Random.Int(1, 3))
                .RuleFor(u => u.ProjectID, GetProjectID());

            return faker.Generate();
        }

        private static Guid GetProjectID()
        {
            var projectTest = new ProjectTest();
            return projectTest.CriarProjeto().Result;
        }
    }
}