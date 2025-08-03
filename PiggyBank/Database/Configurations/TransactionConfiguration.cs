using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PiggyBank.Models;

namespace PiggyBank.Database.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<TransactionModel>
    {
        public void Configure(EntityTypeBuilder<TransactionModel> builder)
        {
            builder.HasOne<WalletModel>().WithMany().HasForeignKey(t => t.WalletId);
            builder.Property(t => t.OccurredAt).HasDefaultValueSql("now()");
        }
    }
}
