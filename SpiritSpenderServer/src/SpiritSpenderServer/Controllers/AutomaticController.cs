using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    }
}