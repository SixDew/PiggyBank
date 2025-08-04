using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PiggyBank.Converters.Interfaces;
using PiggyBank.DTO;
using PiggyBank.Repositories.Interfaces;

namespace PiggyBank.Controllers
{
    [ApiController]
    [Route("api/wallets")]
    public class WalletsController(IValidator<WalletFromClientDto> validator,
        IWalletRepository walletRepository, IWalletConverter converter) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<WalletsListToClientDto>> GetAllWallets(CancellationToken cancellation)
        {
            var wallets = await walletRepository.GetAllAsync(cancellation);
            return Ok(new WalletsListToClientDto() { Data = wallets.Select(converter.Convert).ToList() });

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WalletToClientDto>> GetWallet(Guid id, CancellationToken cancellation)
        {
            var walletModel = await walletRepository.GetAsync(id, cancellation);
            if (walletModel is null) return WalletNotExistProblem();

            return Ok(new WalletToClientDto() { Data = converter.Convert(walletModel) });
        }

        [HttpPost]
        public async Task<ActionResult<WalletToClientDto>> CreateWallet([FromBody] WalletFromClientDto data, CancellationToken cancellation)
        {
            await validator.ValidateAndThrowAsync(data, cancellation);

            var walletModel = await walletRepository.GetAsync(data.Id, cancellation);
            if (walletModel is not null) return WalletAlreadyExistProblem();

            walletModel = converter.Convert(data);
            await walletRepository.AddAsync(walletModel, cancellation);
            await walletRepository.SaveChangesAsync(cancellation);


            return CreatedAtAction(nameof(GetWallet), new { id = walletModel.Id },
                new WalletToClientDto() { Data = converter.Convert(walletModel) });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WalletToClientDto>> RenameWallet([FromBody] string name,
            [FromServices] IValidator<WalletRenameFromClientDto> renameValidator, Guid id, CancellationToken cancellation)
        {
            var walletModel = await walletRepository.GetAsync(id, cancellation);
            if (walletModel is null) return WalletNotExistProblem();

            await renameValidator.ValidateAndThrowAsync(new WalletRenameFromClientDto(name, walletModel),
                cancellation);

            walletModel.Name = name;
            walletRepository.Update(walletModel);
            await walletRepository.SaveChangesAsync(cancellation);

            return Ok(new WalletToClientDto() { Data = converter.Convert(walletModel) });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<WalletToClientDto>> DeleteWallet(Guid id, CancellationToken cancellation)
        {
            var walletModel = await walletRepository.GetAsync(id, cancellation);
            if (walletModel is null) return Ok();
            if (walletModel.Balance != 0) return WalletNotEmptyProblem();

            walletRepository.Delete(walletModel);
            await walletRepository.SaveChangesAsync(cancellation);
            return Ok();
        }

        private ObjectResult WalletNotExistProblem()
        {

            return NotFound(new WalletToClientDto()
            {
                Error = new ProblemDetails()
                {
                    Detail = "Wallet does not exist",
                    Status = 404,
                    Title = "NotFound"
                }
            });
        }

        private ObjectResult WalletAlreadyExistProblem()
        {
            return Conflict(new WalletToClientDto()
            {
                Error = new ProblemDetails()
                {
                    Detail = "Wallet with this Id already exists",
                    Status = 409,
                    Title = "Conflict"
                }
            });
        }

        private ObjectResult WalletNotEmptyProblem()
        {
            return Conflict(new WalletToClientDto()
            {
                Error = new ProblemDetails()
                {
                    Detail = "Wallet is not empty",
                    Status = 409,
                    Title = "Conflict"
                }
            });
        }
    }
}
