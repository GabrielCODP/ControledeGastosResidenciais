using MyExpenseControl.Communication.Response.Category;

namespace MyExpenseControl.Application.UseCases.Category.GetById
{
    public interface IListRecentCategoriesUseCase
    {
        public Task<ResponseCategoriesJson> Execute();
    }
}
