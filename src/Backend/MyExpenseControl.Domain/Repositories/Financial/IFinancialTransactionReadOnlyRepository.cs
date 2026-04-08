namespace MyExpenseControl.Domain.Repositories.Financial
{
    public interface IFinancialTransactionReadOnlyRepository
    {
        Task<IList<Entities.FinancialTransaction>> GetRecentFinancialTransactions(Entities.User user);
        Task<IList<Entities.UserFinancialSummary>> GetUsersFinancialSummary();
    }
}
