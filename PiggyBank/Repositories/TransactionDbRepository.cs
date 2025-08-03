using Microsoft.EntityFrameworkCore;
using PiggyBank.Database;
using PiggyBank.Models;

namespace PiggyBank.Repositories
{
    public class TransactionDbRepository : BaseDbRepository<Transactions, FinancesDbContext>, ITransactionRepository
    {
        public TransactionDbRepository(FinancesDbContext context) : base(context)
        {
        }

        public async Task<List<Transactions>> GetAllAsync(Guid? walletId = null, DateTimeOffset? from = null, DateTimeOffset? to = null)
        {
            var query = Set.AsQueryable();
            if (walletId is not null) query = query.Where(t => t.WalletId == walletId);
            if (from is not null) query = query.Where(t => t.OccurredAt >= from);
            if (to is not null) query = query.Where(t => t.OccurredAt <= to);

            return await query.ToListAsync();
        }
    }
}
