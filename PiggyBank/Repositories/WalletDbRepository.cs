using PiggyBank.Database;
using PiggyBank.Models;

namespace PiggyBank.Repositories
{
    public class WalletDbRepository : BaseDbRepository<Wallets, FinancesDbContext>, IWalletRepository
    {
        public WalletDbRepository(FinancesDbContext context) : base(context)
        {
        }
    }
}
