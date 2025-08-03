using PiggyBank.Models;

namespace PiggyBank.Repositories
{
    public interface ITransactionRepository : IDataRepository<Transactions, Guid>
    {
        Task<List<Transactions>> GetAllAsync(Guid? walletId, DateTimeOffset? from, DateTimeOffset? to);
    }
}
