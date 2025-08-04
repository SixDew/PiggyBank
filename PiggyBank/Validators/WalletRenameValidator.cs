using FluentValidation;
using PiggyBank.DTO;
using PiggyBank.Repositories.Interfaces;

namespace PiggyBank.Validators
{
    public class WalletRenameValidator : AbstractValidator<WalletRenameFromClientDto>
    {
        private readonly IWalletRepository _repository;
        public WalletRenameValidator(IWalletRepository repository)
        {
            _repository = repository;

            RuleFor(w => w.Name).NotNull().MinimumLength(3).MaximumLength(30)
                .WithMessage("Name must be between 3 and 30 characters long");

            RuleFor(w => w.Name).MustAsync(IsUniqName)
                .WithMessage("Name must be uniq");
        }

        private async Task<bool> IsUniqName(WalletRenameFromClientDto walletRenameDto, string name,
            CancellationToken cancellationToken)
        {
            //Проверяем уникальность в одной валюте
            var walletWithNewName = await _repository.FindByNameAsync(name, cancellationToken);
            return walletWithNewName is null || walletWithNewName.Currency != walletRenameDto.OldWallet.Currency;
        }
    }
}
