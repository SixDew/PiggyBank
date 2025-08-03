using Microsoft.AspNetCore.Mvc;
using PiggyBank.Resources;

namespace PiggyBank.DTO
{
    public record TransactionsListDto : BaseDto<List<Transaction>, ProblemDetails>
    {
    }
}
