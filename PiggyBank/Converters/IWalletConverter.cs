using PiggyBank.Models;
using PiggyBank.Resources;

namespace PiggyBank.Converters
{
    public interface IWalletConverter : IConverter<Wallet, Wallets>, IConverter<Wallets, Wallet>
    {
    }
}
