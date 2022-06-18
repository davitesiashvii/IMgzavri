using IMgzavri.Queries.Handlers.Shared;
using IMgzavri.Queries.Queries.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleSoft.Mediator;

namespace IMgzavri.Api.Controllers
{
    [Route("api/shared")]
    [ApiController]
    public class SharedController : BaseController
    {
        public SharedController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("get-cities")]
        public async Task<IActionResult> GetCities(CancellationToken ct)
        {
            var result = await Mediator.FetchAsync(new GetCitiesQuery(), ct);

            return Ok(result);
        }
    }
}
