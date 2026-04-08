
using Bogus;
using Bogus.Extensions;
using MyExpenseControl.Communication.Enums;
using MyExpenseControl.Communication.Requests.Category;


namespace CommonTestUtilities.Requests.Category
{
    public class RequestRegisterCategoryJsonBuilder
    {
        public static RequestRegisterCategoryJson Build()
        {
            return new Faker<RequestRegisterCategoryJson>()
                .RuleFor(category => category.Description, (f) => f.Lorem.Text().ClampLength(1, 200))
                .RuleFor(category => category.Purpose, f => f.PickRandom<Purpose>());
        }
    }
}
