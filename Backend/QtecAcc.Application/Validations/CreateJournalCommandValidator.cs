using FluentValidation;
using QtecAcc.Application.Commands;

namespace QtecAcc.Application.Validations
{
    public class CreateJournalCommandValidator : AbstractValidator<CreateJournalCommand>
    {
        public CreateJournalCommandValidator()
        {
            RuleFor(f => f.Lines)
                            .Must(lines =>
                            {
                                var group = lines.GroupBy(f => f.AccountId).ToList();
                                var debitSum = lines.Sum(l => l.Debit);
                                var creditSum = lines.Sum(l => l.Credit);

                                // Debit and credit totals must match
                                if (debitSum != creditSum || group.Count <= 1 || debitSum <= 0)
                                    return false;

                                // No account should appear in both debit and credit
                                foreach (var g in group)
                                {
                                    var hasDebit = g.Any(l => l.Debit > 0);
                                    var hasCredit = g.Any(l => l.Credit > 0);
                                    if (hasDebit && hasCredit)
                                        return false;
                                }

                                return true;
                            }).WithMessage("The sum of debit and credit must be equal.");

            RuleFor(f => f.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(255).WithMessage("Description must be 255 characters or fewer.")
                .MinimumLength(2).WithMessage("Description must be at least 2 characters.");

            RuleFor(f => f.Date)
                .Must(date => date <= DateTime.Today)
                .WithMessage("Date cannot be in the future.");


        }
    }
}
