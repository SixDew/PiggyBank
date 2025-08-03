using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PiggyBank.Models;

namespace PiggyBank.Database.Configurations
{
    public class WalletConfiguration : IEntityTypeConfiguration<Wallets>
    {
        public void Configure(EntityTypeBuilder<Wallets> builder)
        {
            builder.Property(w => w.CreatedAt).HasDefaultValueSql("now()");
            builder.HasIndex(w => new { w.Name, w.Currency }).IsUnique().HasDatabaseName("UX_Wallets_Name");
        }
    }
}
