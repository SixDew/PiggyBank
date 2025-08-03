using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PiggyBank.DTO;
using PiggyBank.Repositories;
using PiggyBank.Resources;

namespace PiggyBank.Controllers
{
    [ApiController]
    [Route("api/wallets")]
    public class WalletController(IValidator<Wallet> validator, IWalletRepository walletRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<BaseDto>> GetAllWallets()
        {
            var wallets = await walletRepository.GetAllAsync();
            return Ok(new BaseDto() { Data = wallets });

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseDto>> GetWallet()
        {
            return Ok();

        }

        [HttpPost]
        public async Task<ActionResult<Wallet>> CreateWallet([FromBody] Wallet walletData)
        {
            var validationResult = validator.Validate(walletData);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            return Ok(walletData);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseDto>> RenameWallet()
        {
            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseDto>> DeleteWallet()
        {
            return Ok();

        }
    }
}
