using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpiritSpenderServer.Config.HardwareConfiguration;
using SpiritSpenderServer.HardwareControl.EmergencyStop;
using SpiritSpenderServer.Persistence.StatusLampSettings;

namespace SpiritSpenderServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusLampController : ControllerBase
    {
        private IStatusLamp _statusLamp;

        public StatusLampController(IHardwareConfiguration hardwareConfiguration)
        {
            _statusLamp = hardwareConfiguration.StatusLamp;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return new ObjectResult(_statusLamp.StatusLampSetting.Name);
        }

        [HttpGet("setting")]
        public ActionResult<StatusLampSetting> GetSetting()
        {
            var setting = _statusLamp.StatusLampSetting;
            if (setting == null)
                return new NotFoundResult();

            return new ObjectResult(setting);
        }

        [HttpPut("setting")]
        public async Task<ActionResult<StatusLampSetting>> Put([FromBody] StatusLampSetting setting)
        {
            await _statusLamp.UpdateSettings(setting);
            return new OkObjectResult(setting);
        }

        [HttpPost("red-light/on")]
        public ActionResult RedLightOn()
        {
            _statusLamp.RedLightOn();
            return new OkObjectResult(new OkResult());
        }

        [HttpPost("red-light/off")]
        public ActionResult RedLightOff()
        {
            _statusLamp.RedLightOff();
            return new OkObjectResult(new OkResult());
        }

        [HttpPost("red-light/blink")]
        public ActionResult RedLightBlink()
        {
            _statusLamp.RedLightBlink();
            return new OkObjectResult(new OkResult());
        }

        [HttpPost("green-light/on")]
        public ActionResult GreenLightOn()
        {
            _statusLamp.GreenLightOn();
            return new OkObjectResult(new OkResult());
        }

        [HttpPost("green-light/off")]
        public ActionResult GreenLightOff()
        {
            _statusLamp.GreenLightOff();
            return new OkObjectResult(new OkResult());
        }

        [HttpPost("green-light/blink")]
        public ActionResult GreenLightBlink()
        {
            _statusLamp.RedLightBlink();
            return new OkObjectResult(new OkResult());
        }
    }
}
