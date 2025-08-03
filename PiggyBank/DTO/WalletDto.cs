using Microsoft.AspNetCore.Mvc;
using PiggyBank.Resources;

namespace PiggyBank.DTO
{
    public record WalletDto : BaseDto<Wallet, ProblemDetails>
    {
    }
}
