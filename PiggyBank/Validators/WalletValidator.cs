using FluentValidation;
using PiggyBank.Resources;

namespace PiggyBank.Validators
{
    public class WalletValidator : AbstractValidator<Wallet>
    {
        public WalletValidator()
        {
            RuleFor(w => w.Name).NotNull().MinimumLength(3).MaximumLength(30).WithMessage("Name must be between 3 and 30 characters long");
            RuleFor(w => w.Currency).NotNull().Length(3).Matches("[A-Z]{3}").WithMessage("Currency must match ISO 4217 format");
            RuleFor(w => w.Id).NotEmpty();
        }
    }
}
