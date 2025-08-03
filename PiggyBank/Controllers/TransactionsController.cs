using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PiggyBank.DTO;
using PiggyBank.Resources;

namespace PiggyBank.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionsController(IValidator<Transaction> valisdator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<BaseDto>> GetTransactions()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<BaseDto>> AddTransaction()
        {
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult<BaseDto>> DeleteTransaction()
        {
            return Ok();
        }
    }
}
