using Microsoft.EntityFrameworkCore;
using MyExpenseControl.Domain.Entities;
using MyExpenseControl.Domain.Enum;
using MyExpenseControl.Domain.Repositories.Financial;
using MyExpenseControl.Infrastructure.DataAccess.Database;

namespace MyExpenseControl.Infrastructure.DataAccess.Repositories
{
    public class FinancialTransactionRepository : IFinancialTransactionWriteOnlyRepository, IFinancialTransactionReadOnlyRepository
    {
        private readonly MyExpenseControlDbContext _dbContext;
        public FinancialTransactionRepository(MyExpenseControlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(FinancialTransaction financialTransaction)
        {
            await _dbContext.AddAsync(financialTransaction);
        }

        public async Task<IList<FinancialTransaction>> GetRecentFinancialTransactions(User user)
        {
            return await _dbContext.FinancialTransactions
                 .AsNoTracking()
                 .Where(transactions => transactions.Active && transactions.UserId == user.Id)
                 .OrderByDescending(r => r.CreatedOn)
                 .Take(10)
                 .ToListAsync();


        }

        public async Task<IList<UserFinancialSummary>> GetUsersFinancialSummary()
        {
            return await _dbContext.Users
                .Select(user => new 
                {
                    user.Name,
                    TotalRevenue = user.FinancialTransaction
                                  .Where(transaction => transaction.TransactionType == TransactionType.Revenue)
                                  .Sum(transaction => (decimal?)transaction.Amount) ?? 0,
                    TotalExpense = user.FinancialTransaction
                                .Where(transaction => transaction.TransactionType == TransactionType.Expense)
                                .Sum(transaction => (decimal?)transaction.Amount) ?? 0,

                }).Select(summary => new UserFinancialSummary 
                { 
                    Name = summary.Name,
                    TotalRevenue = summary.TotalRevenue,
                    TotalExpense = summary.TotalExpense,
                    Balance = summary.TotalRevenue - summary.TotalExpense
                }).ToListAsync();
        }
    }
}
