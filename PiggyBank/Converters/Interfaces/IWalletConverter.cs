using PiggyBank.DTO;
using PiggyBank.Models;

namespace PiggyBank.Converters.Interfaces
{
    public interface IWalletConverter : IConverter<WalletFromClientDto, Wallets>, IConverter<Wallets, WalletFromClientDto>
    {
    }
}
