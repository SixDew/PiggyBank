using FluentValidation;

namespace PiggyBank.Resources
{
    public class TransactionValidator : AbstractValidator<Transaction>
    {
        public TransactionValidator()
        {
            RuleFor(t => t.Amount).GreaterThan(0).WithMessage("Amount must be positive");
            RuleFor(t => t.OccuredAt).NotNull().Must(date => date <= DateTimeOffset.UtcNow).WithMessage("Date cannot be in the future");
            RuleFor(t => t.Direction).Must(d => d == TransactionDirection.Income || d == TransactionDirection.Expense)
                .WithMessage("Direction must be \"Income\" or \"Expense\"");
        }
    }
}
