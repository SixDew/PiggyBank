using PiggyBank.Database;
using PiggyBank.Models;

namespace PiggyBank.Repositories
{
    public class TransactionDbRepository : BaseDbRepository<TransactionModel, FinancesDbContext>, ITransactionRepository
    {
        public TransactionDbRepository(FinancesDbContext context) : base(context)
        {
        }
    }
}
