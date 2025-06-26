using FluentValidation;
using QtecAcc.Application.DTOs;


namespace QtecAcc.Application.Validations
{
    public class CreateAccountDtoValidator : AbstractValidator<CreateAccountDto>
    {
        public CreateAccountDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Account name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Account type is required.")
                .Must(type => new[] { "Asset", "Liability", "Equity", "Revenue", "Expense" }
                    .Contains(type))
                .WithMessage("Invalid account type.");
        }
    }
}
