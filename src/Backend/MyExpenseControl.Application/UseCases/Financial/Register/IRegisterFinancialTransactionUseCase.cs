using MyExpenseControl.Communication.Requests.Financial;
using MyExpenseControl.Communication.Response.Financial;

namespace MyExpenseControl.Application.UseCases.Financial.Register
{
    public interface IRegisterFinancialTransactionUseCase
    {
        public Task<ResponseRegisterFinancialTransactionJson> Execute(RequestRegisterFinancialTransactionJson request);
    }
}
