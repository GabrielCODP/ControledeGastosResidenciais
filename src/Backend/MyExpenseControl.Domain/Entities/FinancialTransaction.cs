using MyExpenseControl.Domain.Enum;

namespace MyExpenseControl.Domain.Entities
{
    public class FinancialTransaction : EntityBase
    {
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public long CategoryId { get; set; }
        public long UserId { get; set; }
    }
}
