using Microsoft.AspNetCore.Mvc;

namespace PiggyBank.DTO
{
    public record BalanceCounterToClientDto : BaseToClientDto<int, ProblemDetails>
    {
    }
}
