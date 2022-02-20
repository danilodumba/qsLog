using Bogus;
using qsLog.Applications.Models;

namespace qsLog.Test.Integration.Projects
{
    public static class ProjectMock
    {
        public static ProjectModel GetProjectModel()
        {
            var faker = new Faker<ProjectModel>("pt_BR")
                .RuleFor(u => u.Name, f => f.Lorem.Sentence(3));

            return faker.Generate();
        }
    }
}