using Microsoft.AspNetCore.Mvc;

namespace PiggyBank.DTO
{
    public record WalletsListToClientDto : BaseToClientDto<List<WalletFromClientDto>, ProblemDetails>
    {
    }
}
