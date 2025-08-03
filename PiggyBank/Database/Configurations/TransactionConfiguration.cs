using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PiggyBank.Models;

namespace PiggyBank.Database.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transactions>
    {
        public void Configure(EntityTypeBuilder<Transactions> builder)
        {
            builder.HasOne<Wallets>().WithMany().HasForeignKey(t => t.WalletId);
            builder.Property(t => t.CreatedAt).HasDefaultValueSql("now()");
            builder.HasIndex(t => new { t.WalletId, t.OccurredAt }).HasDatabaseName("IX_Transactions_WalletId_OccurredAt");
        }
    }
}
