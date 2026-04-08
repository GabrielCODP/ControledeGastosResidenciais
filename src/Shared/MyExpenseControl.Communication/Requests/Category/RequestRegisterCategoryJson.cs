using MyExpenseControl.Communication.Enums;

namespace MyExpenseControl.Communication.Requests.Category
{
    public class RequestRegisterCategoryJson
    {
        public string Description { get; set; } = string.Empty;
        public Purpose Purpose { get; set; }
    }
}
