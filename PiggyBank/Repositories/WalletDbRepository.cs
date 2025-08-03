using PiggyBank.Database;
using PiggyBank.Models;

namespace PiggyBank.Repositories
{
    public class WalletDbRepository : BaseDbRepository<WalletModel, FinancesDbContext>, IWalletRepository
    {
        public WalletDbRepository(FinancesDbContext context) : base(context)
        {
        }
    }
}
