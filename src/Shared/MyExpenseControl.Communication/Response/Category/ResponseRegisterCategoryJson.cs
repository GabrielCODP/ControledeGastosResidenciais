using MyExpenseControl.Communication.Enums;

namespace MyExpenseControl.Communication.Response.Category
{
    public class ResponseRegisterCategoryJson
    {
        public string Id { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Purpose Purpose { get; set; }
    }
}
