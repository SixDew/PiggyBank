using PiggyBank.Converters.Interfaces;
using PiggyBank.DTO;
using PiggyBank.Models;

namespace PiggyBank.Converters
{
    public class WalletConverter : IWalletConverter
    {
        public Wallets Convert(WalletFromClientDto data)
        {
            return new()
            {
                Balance = data.Balance,
                Currency = data.Currency,
                Name = data.Name,
                Id = data.Id
            };
        }

        public WalletFromClientDto Convert(Wallets data)
        {
            return new(
                Id: data.Id,
                Balance: data.Balance,
                Currency: data.Currency,
                Name: data.Name);
        }
    }
}
