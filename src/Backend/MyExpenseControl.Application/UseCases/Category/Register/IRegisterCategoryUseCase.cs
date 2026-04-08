using MyExpenseControl.Communication.Requests.Category;
using MyExpenseControl.Communication.Response.Category;

namespace MyExpenseControl.Application.UseCases.Category.Register
{
    public interface IRegisterCategoryUseCase
    {
        public Task<ResponseRegisterCategoryJson> Execute(RequestRegisterCategoryJson request);
    }
}
