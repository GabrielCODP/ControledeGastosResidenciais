using FluentValidation;
using MyExpenseControl.Application.SharedValidators;
using MyExpenseControl.Communication.Requests.User;
using MyExpenseControl.Domain.Entities;
using MyExpenseControl.Exceptions;

namespace MyExpenseControl.Application.UseCases.User.Register
{
    public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
            RuleFor(user => user.Age).GreaterThan(0).WithMessage(ResourceMessagesException.AGE_INVALID);
            RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY);
            RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestRegisterUserJson>());
            When(user => string.IsNullOrEmpty(user.Email) == false, () =>
            {
                RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);
            });
            When(user => string.IsNullOrEmpty(user.Name) == false, () =>
            {
                RuleFor(user => user.Name).MinimumLength(3).MaximumLength(200).WithMessage(ResourceMessagesException.NAME_MAXIMUM);
            });

        }
    }
}


