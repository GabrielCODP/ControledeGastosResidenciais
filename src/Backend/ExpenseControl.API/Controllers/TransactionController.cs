using ExpenseControl.API.Attributes;
using Microsoft.AspNetCore.Mvc;
using MyExpenseControl.Application.UseCases.Financial.GetTransaction;
using MyExpenseControl.Application.UseCases.Financial.Register;
using MyExpenseControl.Communication.Requests.Financial;
using MyExpenseControl.Communication.Response.Financial;

namespace ExpenseControl.API.Controllers
{
    [AuthenticatedUser]
    public class TransactionController : ExpenseControlBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisterFinancialTransactionJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register([FromServices] IRegisterFinancialTransactionUseCase useCase, [FromBody] RequestRegisterFinancialTransactionJson request)
        {
            var result = await useCase.Execute(request);

            return Created(string.Empty, result);
        }

        [HttpGet("RecentFinancialTransactions")]
        [ProducesResponseType(typeof(ResponseFinancialTransactionsJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get([FromServices] IGetRecentFinancialTransactionsUseCase useCase)
        {
            var response = await useCase.Execute();

            if (response.Transactions.Any()) 
            {
                return Ok(response);
            }

            return NoContent();
        }
    }
}
