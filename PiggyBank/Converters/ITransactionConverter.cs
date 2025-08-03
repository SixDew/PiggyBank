using PiggyBank.Models;
using PiggyBank.Resources;

namespace PiggyBank.Converters
{
    public interface ITransactionConverter : IConverter<Transaction, Transactions>, IConverter<Transactions, Transaction>
    {
    }
}
