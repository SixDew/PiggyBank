using PiggyBank.Models;

namespace PiggyBank.Repositories.Interfaces
{
    public interface ITransactionRepository : IDataRepository<Transactions, Guid>
    {
        Task<List<Transactions>> GetAllAsync(CancellationToken cancellation, Guid? walletId = null,
            DateTimeOffset? from = null, DateTimeOffset? to = null);
    }
}
