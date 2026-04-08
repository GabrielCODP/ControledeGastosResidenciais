using MyExpenseControl.Communication.Response.Financial.FinancialSummary;

namespace MyExpenseControl.Application.UseCases.Financial.GetPeopleFinancialSumary
{
    public interface IGetPeopleFinancialSummaryUseCase
    {
        public Task<ResponsePeopleFinancialSummaryJson> Execute();
    }
}
