using Bogus;
using MyExpenseControl.Communication.Requests.User;

namespace CommonTestUtilities.Requests.User
{
    public class RequestRegisterUserJsonBuilder
    {
        public static RequestRegisterUserJson Build(int passwordLength = 10)
        {
            return new Faker<RequestRegisterUserJson>()
                .RuleFor(user => user.Name, (f) => f.Person.FirstName)
                .RuleFor(user => user.Age, (f) => f.Random.Int(1, 120))
                .RuleFor(user => user.Email, (f, u) => f.Internet.Email(u.Name))
                .RuleFor(user => user.Password, (f) => f.Internet.Password(passwordLength));
        }
    }
}
