using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyExpenseControl.Application.UseCases.Financial.GetPeopleFinancialSumary;
using MyExpenseControl.Communication.Response.Financial.FinancialSummary;

namespace ExpenseControl.API.Controllers
{
    public class PeopleFinancialSummaryController : ExpenseControlBaseController
    {
        [HttpGet]
        [ProducesResponseType(typeof(ResponsePersonFinancialSummaryJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get([FromServices] IGetPeopleFinancialSummaryUseCase useCase) 
        {
            var response = await useCase.Execute();

            if (response.People.Any())
            {
                return Ok(response);
            }

            return NoContent();
        }

    }
}
