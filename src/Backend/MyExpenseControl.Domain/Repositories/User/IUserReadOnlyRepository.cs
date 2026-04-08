namespace MyExpenseControl.Domain.Repositories.User
{
    public interface IUserReadOnlyRepository
    {
        public Task<bool> ExisteActiveUserWithEmail(string email);
        public Task<Entities.User?> GetByEmailAndPassword(string email, string password);
        public Task<bool> ExisteActiveUserWithIdentifier(Guid userIdentifier);
    }
}
