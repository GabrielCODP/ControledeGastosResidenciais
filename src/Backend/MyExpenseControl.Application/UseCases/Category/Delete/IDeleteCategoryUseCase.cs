namespace MyExpenseControl.Application.UseCases.Category.Delete
{
    public interface IDeleteCategoryUseCase
    {
        public Task Execute(long categoryId);
    }
}
