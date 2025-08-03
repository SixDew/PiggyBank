using FluentValidation;
using PiggyBank.DTO;

namespace PiggyBank.Validators
{
    public class WalletRenameValidator : AbstractValidator<WalletRenameDto>
    {
        public WalletRenameValidator()
        {
            RuleFor(s => s.Name).NotNull().MinimumLength(3).MaximumLength(30).WithMessage("Name must be between 3 and 30 characters long");
        }
    }
}
