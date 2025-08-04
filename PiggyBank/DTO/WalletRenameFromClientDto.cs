using PiggyBank.Models;

namespace PiggyBank.DTO
{
    public record WalletRenameFromClientDto(string Name, Wallets OldWallet)
    {
    }
}
