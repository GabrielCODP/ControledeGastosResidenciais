
using Microsoft.EntityFrameworkCore;
using MyExpenseControl.Domain.Entities;

namespace MyExpenseControl.Infrastructure.DataAccess.Database
{
    public class MyExpenseControlDbContext : DbContext
    {
        public MyExpenseControlDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FinancialTransaction> FinancialTransactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyExpenseControlDbContext).Assembly);
        }
    }
}
