using Moq;
using MyExpenseControl.Domain.Repositories.Category;

namespace CommonTestUtilities.Repository
{
    public class CategoryWriteOnlyRepositoryBuilder
    {
        public static ICategoryWriteOnlyRepository Build()
        {
            var mock = new Mock<ICategoryWriteOnlyRepository>();
            return mock.Object;
        }
    }
}
