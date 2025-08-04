using Microsoft.AspNetCore.Mvc;

namespace PiggyBank.DTO
{
    public record TransactionToClientDto : BaseToClientDto<TransactionFromClientDto, ProblemDetails>
    {
    }
}
