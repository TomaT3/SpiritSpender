using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpiritSpenderServer.Automatic;

namespace SpiritSpenderServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutomaticController : ControllerBase
    {
        private readonly IAutomaticMode _automaticMode;

        public AutomaticController(IAutomaticMode automaticMode)
            => (_automaticMode) = (automaticMode);

        [HttpPost("release-the-spirit")]
        public async Task<ActionResult> DriveToPosition()
        {
            await _automaticMode.ReleaseTheSpiritAsync();
            return new OkObjectResult(new OkResult());
        }

        [HttpPost("reference-all-axis")]
        public async Task<ActionResult> ReferenceAllAxis()
        {
            await _automaticMode.ReferenceAllAxis();
            return new OkObjectResult(new OkResult());
        }
    }
}