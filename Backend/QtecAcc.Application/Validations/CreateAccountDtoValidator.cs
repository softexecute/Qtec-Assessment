using FluentValidation;
using QtecAcc.Application.Commands;
using QtecAcc.Application.DTOs;


namespace QtecAcc.Application.Validations
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Account name is required.")
                .MinimumLength(3).WithMessage("Account name must be at least 3 characters.")
                .MaximumLength(100);

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Account type is required.")
                .Must(type => new[] { "Asset", "Liability", "Equity", "Revenue", "Expense" }
                    .Contains(type))
                .WithMessage("Invalid account type.");
        }
    }
}
