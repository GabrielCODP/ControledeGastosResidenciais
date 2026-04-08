namespace MyExpenseControl.Domain.Repositories.Category
{
    public interface ICategoryWriteOnlyRepository
    {
        public Task Add(Entities.Category category);
        public Task Delete(long categoryId);
    }
}
