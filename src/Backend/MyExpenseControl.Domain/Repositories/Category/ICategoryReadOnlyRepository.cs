namespace MyExpenseControl.Domain.Repositories.Category
{
    public interface ICategoryReadOnlyRepository
    {
        Task<IList<Entities.Category>> GetRecentlyCreatedCategories(Entities.User user);
        Task<Entities.Category?> GetById(Entities.User user, long categoryId);
    }
}
