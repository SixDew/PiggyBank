using Microsoft.AspNetCore.Mvc;

namespace PiggyBank.DTO
{
    public record TransactionsListToClientDto : BaseToClientDto<List<TransactionFromClientDto>, ProblemDetails>
    {
    }
}
