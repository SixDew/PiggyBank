using PiggyBank.Models;
using PiggyBank.Resources;

namespace PiggyBank.Converters
{
    public class TransactionConverter : ITransactionConverter
    {
        public Transactions Convert(Transaction data)
        {
            return new()
            {
                Amount = data.Amount,
                Direction = data.Direction,
                OccurredAt = data.OccuredAt,
                Id = data.Id,
                WalletId = data.WalletId,
                Comment = data.Comment
            };
        }

        public Transaction Convert(Transactions data)
        {
            return new(
                Amount: data.Amount,
                Comment: data.Comment,
                Direction: data.Direction,
                Id: data.Id,
                OccuredAt: data.OccurredAt,
                WalletId: data.WalletId);
        }
    }
}
