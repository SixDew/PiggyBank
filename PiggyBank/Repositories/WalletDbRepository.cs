using Microsoft.EntityFrameworkCore;
using PiggyBank.Database;
using PiggyBank.Models;
using PiggyBank.Repositories.Abstract;
using PiggyBank.Repositories.Interfaces;

namespace PiggyBank.Repositories
{
    public class WalletDbRepository : BaseDbRepository<Wallets, FinancesDbContext>, IWalletRepository
    {
        public WalletDbRepository(FinancesDbContext context) : base(context)
        {
        }

        public async Task<Wallets?> FindByNameAsync(string Name, CancellationToken cancellation)
        {
            return await Set.FirstOrDefaultAsync(x => x.Name == Name, cancellation);
        }
    }
}
