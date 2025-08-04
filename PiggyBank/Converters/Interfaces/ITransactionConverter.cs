using PiggyBank.DTO;
using PiggyBank.Models;

namespace PiggyBank.Converters.Interfaces
{
    public interface ITransactionConverter : IConverter<TransactionFromClientDto, Transactions>, IConverter<Transactions, TransactionFromClientDto>
    {
    }
}
