using Microsoft.EntityFrameworkCore;
using PiggyBank.Database.Configurations;
using PiggyBank.Models;

namespace PiggyBank.Database
{
    public class FinancesDbContext : DbContext
    {
        public FinancesDbContext(DbContextOptions<FinancesDbContext> options) : base(options) { }
        public DbSet<Wallets> Wallets = null!;
        public DbSet<Transactions> Transactions = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new WalletConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
