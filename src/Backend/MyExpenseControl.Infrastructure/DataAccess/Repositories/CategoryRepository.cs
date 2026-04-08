using Microsoft.EntityFrameworkCore;
using MyExpenseControl.Domain.Entities;
using MyExpenseControl.Domain.Repositories.Category;
using MyExpenseControl.Infrastructure.DataAccess.Database;

namespace MyExpenseControl.Infrastructure.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryWriteOnlyRepository, ICategoryReadOnlyRepository
    {
        private readonly MyExpenseControlDbContext _dbContext;

        public CategoryRepository(MyExpenseControlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
        }

        public async Task Delete(long categoryId)
        {
            var category = await _dbContext.Categories.FindAsync(categoryId);

            _dbContext.Categories.Remove(category!);
        }

        public async Task<Category?> GetById(User user, long categoryId)
        {
            return await _dbContext.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Active && c.Id == categoryId && c.UserId == user.Id);
        }

        public async Task<IList<Category>> GetRecentlyCreatedCategories(User user)
        {
            return await _dbContext.Categories
                .AsNoTracking()
                .Where(category => category.Active && category.UserId == user.Id)
                .OrderByDescending(r => r.CreatedOn)
                .Take(5)
                .ToListAsync();
        }
    }
}
