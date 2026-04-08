using MyExpenseControl.Communication.Enums;


namespace MyExpenseControl.Communication.Requests.Financial
{
    public class RequestRegisterFinancialTransactionJson
    {
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public long CategoryId { get; set; }
    }
}
