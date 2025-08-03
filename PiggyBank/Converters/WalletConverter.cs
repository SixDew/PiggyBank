using PiggyBank.Models;
using PiggyBank.Resources;

namespace PiggyBank.Converters
{
    public class WalletConverter : IWalletConverter
    {
        public Wallets Convert(Wallet data)
        {
            return new()
            {
                Balance = data.Balance,
                Currency = data.Currency,
                Name = data.Name,
                Id = data.Id
            };
        }

        public Wallet Convert(Wallets data)
        {
            return new(
                Id: data.Id,
                Balance: data.Balance,
                Currency: data.Currency,
                Name: data.Name);
        }
    }
}
