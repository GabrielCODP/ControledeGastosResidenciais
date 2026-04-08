using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyExpenseControl.Application.UseCases.User.DoLogin;
using MyExpenseControl.Communication.Requests.User;
using MyExpenseControl.Communication.Response;
using MyExpenseControl.Communication.Response.Users;

namespace ExpenseControl.API.Controllers
{
    public class LoginController : ExpenseControlBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromServices] IDoLoginUseCase useCase, [FromBody] RequestLoginJson request)
        {
            var response = await useCase.Execute(request);

            return Ok(response);
        }

    }
}
