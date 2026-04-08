using MyExpenseControl.Communication.Response.Financial;

namespace MyExpenseControl.Application.UseCases.Financial.GetTransaction
{
    public interface IGetRecentFinancialTransactionsUseCase
    {
        public Task<ResponseFinancialTransactionsJson> Execute();
    }
}
