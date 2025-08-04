using Microsoft.AspNetCore.Mvc;

namespace PiggyBank.DTO
{
    public record WalletToClientDto : BaseToClientDto<WalletFromClientDto, ProblemDetails>
    {
    }
}
