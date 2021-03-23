using Bogus;
using qsLog.Applications.Models;

namespace qsLog.Test.Integration.Users
{
    public static class UserMock
    {
        public static UserModel GetUserModel()
        {
            var faker = new Faker<UserModel>("pt_BR")
                .RuleFor(u => u.ConfirmPassword, "abcd")
                .RuleFor(u => u.Password, "abcd")
                .RuleFor(u => u.Name, f => f.Person.FullName)
                .RuleFor(u => u.UserName, f => f.Person.UserName)
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(u => u.Administrator, f => f.Random.Bool());

            return faker.Generate();
        }
    }
}