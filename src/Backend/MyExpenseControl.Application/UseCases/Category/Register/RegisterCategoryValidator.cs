using FluentValidation;
using MyExpenseControl.Communication.Requests.Category;
using MyExpenseControl.Exceptions;

namespace MyExpenseControl.Application.UseCases.Category.Register
{
    public class RegisterCategoryValidator : AbstractValidator<RequestRegisterCategoryJson>
    {
        public RegisterCategoryValidator() 
        {
            RuleFor(category => category.Description).NotEmpty().WithMessage(ResourceMessagesException.CATEGORY_EMPTY)
                  .MaximumLength(400).WithMessage(ResourceMessagesException.CATEGORY_DESCRIPTION_MAX_LENGTH_400);
            RuleFor(category => category.Purpose).IsInEnum().WithMessage(ResourceMessagesException.PURPOSE_NOT_SUPPORTED);
        }
    }
}
