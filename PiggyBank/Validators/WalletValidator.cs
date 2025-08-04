using FluentValidation;
using PiggyBank.DTO;
using PiggyBank.Repositories.Interfaces;

namespace PiggyBank.Validators
{
    public class WalletValidator : AbstractValidator<WalletFromClientDto>
    {
        private readonly IWalletRepository _repository;
        public WalletValidator(IWalletRepository repository)
        {
            _repository = repository;

            RuleFor(w => w.Name).NotNull().MinimumLength(3).MaximumLength(30)
                .WithMessage("Name must be between 3 and 30 characters long");

            RuleFor(w => w.Currency).NotNull().Length(3).Matches("[A-Z]{3}")
                .WithMessage("Currency must match ISO 4217 format");

            RuleFor(w => w.Id).NotEmpty();

            RuleFor(w => w.Name).MustAsync(IsUniqName)
                .WithMessage("Name must be unique");
        }

        private async Task<bool> IsUniqName(WalletFromClientDto wallet, string name, CancellationToken cancellationToken)
        {
            //Проверяем уникальность в одной валюте
            var walletFromDb = await _repository.FindByNameAsync(name, cancellationToken);
            return walletFromDb is null || walletFromDb.Currency != wallet.Currency;
        }
    }
}
