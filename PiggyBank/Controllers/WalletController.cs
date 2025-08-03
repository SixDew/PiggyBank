using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PiggyBank.Converters;
using PiggyBank.DTO;
using PiggyBank.Repositories;
using PiggyBank.Resources;

namespace PiggyBank.Controllers
{
    [ApiController]
    [Route("api/wallets")]
    public class WalletController(IValidator<Wallet> validator, IWalletRepository walletRepository, IWalletConverter converter) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<WalletsListDto>> GetAllWallets()
        {
            var wallets = await walletRepository.GetAllAsync();
            return Ok(new WalletsListDto() { Data = wallets.Select(converter.Convert).ToList() });

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WalletDto>> GetWallet(Guid id)
        {
            var walletModel = await walletRepository.GetAsync(id);
            if (walletModel is null) return WalletNotExistProblem();

            return Ok(new WalletDto() { Data = converter.Convert(walletModel) });
        }

        [HttpPost]
        public async Task<ActionResult<WalletDto>> CreateWallet([FromBody] Wallet data)
        {
            validator.ValidateAndThrow(data);

            var walletModel = await walletRepository.GetAsync(data.Id);
            if (walletModel is not null) return WalletAlreadyExistProblem();

            walletModel = converter.Convert(data);
            await walletRepository.AddAsync(walletModel);
            await walletRepository.SaveChangesAsync();


            return CreatedAtAction(nameof(GetWallet), new { id = walletModel.Id },
                new WalletDto() { Data = converter.Convert(walletModel) });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WalletDto>> RenameWallet([FromBody] WalletRenameDto data,
            [FromServices] IValidator<WalletRenameDto> renameValidator, Guid id)
        {
            renameValidator.ValidateAndThrow(data);

            var walletModel = await walletRepository.GetAsync(id);
            if (walletModel is null) return WalletNotExistProblem();

            walletModel.Name = data.Name;
            walletRepository.Update(walletModel);
            await walletRepository.SaveChangesAsync();

            return Ok(new WalletDto() { Data = converter.Convert(walletModel) });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<WalletDto>> DeleteWallet(Guid id)
        {
            var walletModel = await walletRepository.GetAsync(id);
            if (walletModel is null) return Ok();
            if (walletModel.Balance != 0) return WalletNotEmptyProblem();

            walletRepository.Delete(walletModel);
            await walletRepository.SaveChangesAsync();
            return Ok();
        }

        private ObjectResult WalletNotExistProblem()
        {

            return NotFound(new WalletDto()
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
            return Conflict(new WalletDto()
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
            return Conflict(new WalletDto()
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
