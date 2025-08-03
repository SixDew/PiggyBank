using Microsoft.EntityFrameworkCore;
using PiggyBank.Database.Configurations;
using PiggyBank.Models;

namespace PiggyBank.Database
{
    public class FinancesDbContext : DbContext
    {
        public FinancesDbContext(DbContextOptions<FinancesDbContext> options) : base(options) { }
        public DbSet<WalletModel> Wallets = null!;
        public DbSet<TransactionModel> Transactions = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
