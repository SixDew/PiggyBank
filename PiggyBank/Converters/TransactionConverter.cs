using PiggyBank.Converters.Interfaces;
using PiggyBank.DTO;
using PiggyBank.Models;

namespace PiggyBank.Converters
{
    public class TransactionConverter : ITransactionConverter
    {
        public Transactions Convert(TransactionFromClientDto data)
        {
            return new()
            {
                Amount = data.Amount,
                Direction = (TransactionDirection)data.Direction!,
                OccurredAt = data.OccuredAt,
                Id = data.Id,
                WalletId = data.WalletId,
                Comment = data.Comment
            };
        }

        public TransactionFromClientDto Convert(Transactions data)
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
