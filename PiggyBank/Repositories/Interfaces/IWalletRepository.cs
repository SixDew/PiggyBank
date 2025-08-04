using PiggyBank.Models;

namespace PiggyBank.Repositories.Interfaces
{
    public interface IWalletRepository : IDataRepository<Wallets, Guid>
    {
        Task<Wallets?> FindByNameAsync(string Name, CancellationToken cancellation);
    }
}
