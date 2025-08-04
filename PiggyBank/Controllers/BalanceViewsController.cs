using Microsoft.AspNetCore.Mvc;
using PiggyBank.DTO;
using PiggyBank.Services.Interfaces;

namespace PiggyBank.Controllers
{
    [ApiController]
    [Route("api/balance-views")]
    public class BalanceViewsController(IBalanceCounter counter) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<BalanceCounterToClientDto>> GetViews(CancellationToken cancellation)
        {
            return Ok(new BalanceCounterToClientDto() { Data = counter.Veiws });
        }

        [HttpPut]
        public async Task<ActionResult<BalanceCounterToClientDto>> IncrementViews(CancellationToken cancellation)
        {
            counter.IncViews();
            return Ok(new BalanceCounterToClientDto() { Data = counter.Veiws });
        }

    }
}
