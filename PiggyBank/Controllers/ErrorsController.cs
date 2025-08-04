using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PiggyBank.Controllers
{
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        [Route("error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult ErrorHandle(CancellationToken cancellation)
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            if (exception is ValidationException ex)
            {
                //Разбиваем по названию свойств
                var errors = ex.Errors.GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage));

                return Problem(
                    title: "One or more validation errors occurred.",
                    type: "https://httpstatuses.com/400",
                    extensions: new Dictionary<string, object?> { ["errors"] = errors },
                    statusCode: 400
                    );
            }

            return Problem(detail: exception?.Message);
        }
    }
}
