using Moq;
using MyExpenseControl.Domain.Entities;
using MyExpenseControl.Domain.Services.LoggedUser;

namespace CommonTestUtilities.LoggedUserBuilder
{
    public class LoggedUserBuilder
    {
        public static ILoggedUser Build(User user)
        {
            var mock = new Mock<ILoggedUser>();
            mock.Setup(X => X.User()).ReturnsAsync(user);

            return mock.Object;
        }
    }
}
