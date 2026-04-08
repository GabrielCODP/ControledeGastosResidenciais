using MyExpenseControl.Communication.Enums;

namespace MyExpenseControl.Communication.Response.Financial
{
    public class ResponseRegisterFinancialTransactionJson
    {
        public string Id { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
