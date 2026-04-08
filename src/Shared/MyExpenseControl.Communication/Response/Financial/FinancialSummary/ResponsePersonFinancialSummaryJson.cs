
namespace MyExpenseControl.Communication.Response.Financial.FinancialSummary
{
    public class ResponsePersonFinancialSummaryJson
    {
        public string Name { get; set; } = string.Empty;
        public decimal TotalRevenue { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Balance { get; set; }
    }
}
