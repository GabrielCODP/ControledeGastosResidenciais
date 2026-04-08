

namespace MyExpenseControl.Communication.Response.Financial
{
    public class ResponseFinancialTransactionsJson
    {
        public IList<ResponseShortFinancialTransactionJson> Transactions { get; set; } = [];
    }
}
