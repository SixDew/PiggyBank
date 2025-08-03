using Microsoft.AspNetCore.Mvc;
using PiggyBank.Resources;

namespace PiggyBank.DTO
{
    public record WalletsListDto : BaseDto<List<Wallet>, ProblemDetails>
    {
    }
}
