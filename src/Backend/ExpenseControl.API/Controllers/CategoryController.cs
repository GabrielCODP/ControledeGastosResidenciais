using ExpenseControl.API.Attributes;
using Microsoft.AspNetCore.Mvc;
using MyExpenseControl.Application.UseCases.Category.Delete;
using MyExpenseControl.Application.UseCases.Category.GetById;
using MyExpenseControl.Application.UseCases.Category.Register;
using MyExpenseControl.Communication.Requests.Category;
using MyExpenseControl.Communication.Response;
using MyExpenseControl.Communication.Response.Category;

namespace ExpenseControl.API.Controllers
{
    [AuthenticatedUser]
    public class CategoryController : ExpenseControlBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisterCategoryJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register([FromServices] IRegisterCategoryUseCase useCase, [FromBody] RequestRegisterCategoryJson request)
        {
            var result = await useCase.Execute(request);

            return Created(string.Empty, result);
        }

        [HttpGet("RecentCategories")]
        [ProducesResponseType(typeof(ResponseCategoriesJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get([FromServices] IListRecentCategoriesUseCase useCase)
        {
            var response = await useCase.Execute();

            if (response.Categories.Any())
            {
                return Ok(response);
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromServices] IDeleteCategoryUseCase useCase, long id)
        {
            await useCase.Execute(id);

            return NoContent();
        }


    }
}
