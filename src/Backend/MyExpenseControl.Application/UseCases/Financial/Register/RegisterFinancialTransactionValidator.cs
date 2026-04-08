using FluentValidation;
using MyExpenseControl.Communication.Requests.Financial;
using MyExpenseControl.Exceptions;

namespace MyExpenseControl.Application.UseCases.Financial.Register
{
    public class RegisterFinancialTransactionValidator : AbstractValidator<RequestRegisterFinancialTransactionJson>
    {
        public RegisterFinancialTransactionValidator()
        {
            RuleFor(financial => financial.Description).NotEmpty().WithMessage(ResourceMessagesException.FINANCIAL_EMPTY)
                  .MaximumLength(400).WithMessage(ResourceMessagesException.FINANCIAL_DESCRIPTION_MAX_LENGTH_400);
            RuleFor(financial => financial.TransactionType).IsInEnum().WithMessage(ResourceMessagesException.TRANSACTIONTYPE_NOT_SUPPORTED);
            RuleFor(financial => financial.Amount).GreaterThanOrEqualTo(0)
                .PrecisionScale(18, 2, false)
                .WithMessage((ResourceMessagesException.AMOUNT_INVALID));


        }
    }
}
