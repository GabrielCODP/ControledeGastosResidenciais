using MyExpenseControl.Communication.Enums;
using System.Text.Json.Serialization;

namespace MyExpenseControl.Communication.Response.Financial
{
    public class ResponseShortFinancialTransactionJson
    {
        public string Id { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TransactionType TransactionType { get; set; }
    }
}
