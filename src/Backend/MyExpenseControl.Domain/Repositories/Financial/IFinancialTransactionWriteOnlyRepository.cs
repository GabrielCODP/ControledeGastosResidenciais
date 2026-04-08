
namespace MyExpenseControl.Domain.Repositories.Financial
{
    public interface IFinancialTransactionWriteOnlyRepository
    {
        public Task Add(Entities.FinancialTransaction financialTransaction);
    }
}
