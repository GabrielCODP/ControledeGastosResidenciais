using Microsoft.EntityFrameworkCore;
using MyExpenseControl.Domain.Entities;
using MyExpenseControl.Domain.Repositories.User;
using MyExpenseControl.Infrastructure.DataAccess.Database;

namespace MyExpenseControl.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository, IUserDeleteOnlyRepository
    {
        private readonly MyExpenseControlDbContext _dbContext;

        public UserRepository(MyExpenseControlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(User user)
        {
            await _dbContext.Users.AddAsync(user);
        }

        public void DeleteAccount(User user)
        {
            var financialTransactions = _dbContext.FinancialTransactions.Where(financial => financial.UserId == user.Id);

            //Quando deletar, os resto vai ser deletado. Pq criamos a tabela com OnDeleteCascate;
            _dbContext.FinancialTransactions.RemoveRange(financialTransactions);
            _dbContext.Users.Remove(user);
        }

        public async Task<bool> ExisteActiveUserWithEmail(string email)
        {
            return await _dbContext.Users.AsNoTracking().AnyAsync(user => user.Email.Equals(email) && user.Active);
        }

        public async Task<bool> ExisteActiveUserWithIdentifier(Guid userIdentifier)
        {
            return await _dbContext.Users.AnyAsync(user => user.UserIdentifier.Equals(userIdentifier) && user.Active);
        }

        public async Task<User?> GetByEmailAndPassword(string email, string password)
        {
            return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Active && user.Email.Equals(email) && user.Password.Equals(password));
        }


    }
}
