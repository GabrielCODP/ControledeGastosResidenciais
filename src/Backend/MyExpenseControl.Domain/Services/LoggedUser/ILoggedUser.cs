using MyExpenseControl.Domain.Entities;

namespace MyExpenseControl.Domain.Services.LoggedUser
{
    public interface ILoggedUser
    {
        public Task<User> User();
    }
}
