using MyExpenseControl.Domain.Enum;

namespace MyExpenseControl.Domain.Entities
{
    public class Category : EntityBase
    {
        public string Description { get; set; } = string.Empty;
        public Purpose Purpose { get; set; }
        public long UserId { get; set; }
    }
}
