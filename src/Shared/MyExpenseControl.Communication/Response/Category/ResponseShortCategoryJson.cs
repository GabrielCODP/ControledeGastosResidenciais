using MyExpenseControl.Communication.Enums;
using System.Text.Json.Serialization;

namespace MyExpenseControl.Communication.Response.Category
{
    public class ResponseShortCategoryJson
    {
        public string Id { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Purpose Purpose { get; set; }
    }
}
