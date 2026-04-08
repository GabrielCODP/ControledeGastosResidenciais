using Moq;
using MyExpenseControl.Domain.Repositories.User;

namespace CommonTestUtilities.Repository
{
    public class UserWriteOnlyRepositoryBuilder
    {
        public static IUserWriteOnlyRepository Build()
        {
            var mock = new Mock<IUserWriteOnlyRepository>();
            return mock.Object;
        }
    }
}
