using PiggyBank.Models;

namespace PiggyBank.Repositories
{
    public interface IWalletRepository : IDataRepository<WalletModel, Guid>
    {
    }
}
