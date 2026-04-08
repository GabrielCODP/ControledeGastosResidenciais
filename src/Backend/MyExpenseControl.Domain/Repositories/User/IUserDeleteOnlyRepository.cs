namespace MyExpenseControl.Domain.Repositories.User
{
    public interface IUserDeleteOnlyRepository
    {
        public void DeleteAccount(Entities.User user);
    }
}
