
namespace MyExpenseControl.Domain.Entities
{
    public class UserFinancialSummary
    {
        public string Name { get; set; } = string.Empty;
        public decimal TotalRevenue { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Balance { get; set; }
    }
}
