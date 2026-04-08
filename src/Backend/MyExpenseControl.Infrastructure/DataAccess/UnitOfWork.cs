using MyExpenseControl.Domain.Repositories;
using MyExpenseControl.Infrastructure.DataAccess.Database;

namespace MyExpenseControl.Infrastructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
     
        private readonly MyExpenseControlDbContext _dbContext;
        public UnitOfWork(MyExpenseControlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
