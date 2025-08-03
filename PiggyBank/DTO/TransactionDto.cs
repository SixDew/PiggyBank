using Microsoft.AspNetCore.Mvc;
using PiggyBank.Resources;

namespace PiggyBank.DTO
{
    public record TransactionDto : BaseDto<Transaction, ProblemDetails>
    {
    }
}
