using PiggyBank.Models;

namespace PiggyBank.Repositories
{
    public interface ITransactionRepository : IDataRepository<TransactionModel, Guid>
    {
    }
}
