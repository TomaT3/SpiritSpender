﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpiritSpenderServer.Config.HardwareConfiguration;
using SpiritSpenderServer.HardwareControl.SpiritSpenderMotor;
using SpiritSpenderServer.Persistence.SpiritDispenserSettings;

namespace SpiritSpenderServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpiritDispenserController : ControllerBase
    {
        private ISpiritDispenserControl _spiritDispenserControl;

        public SpiritDispenserController(IHardwareConfiguration hardwareConfiguration)
            => (_spiritDispenserControl) = (hardwareConfiguration.SpiritDispenserControl);

        [HttpGet]
        public ActionResult<string> Get()
        {
            return new ObjectResult(_spiritDispenserControl.SpiritDispenserSetting.Name);
        }

        [HttpGet("setting")]
        public ActionResult<SpiritDispenserSetting> GetSetting()
        {
            return new ObjectResult(_spiritDispenserControl.SpiritDispenserSetting);
        }

        [HttpPut("setting")]
        public async Task<ActionResult<SpiritDispenserSetting>> Put([FromBody] SpiritDispenserSetting spiritDispenserSetting)
        {
            await _spiritDispenserControl.UpdateSettingsAsync(spiritDispenserSetting);
            return new OkObjectResult(_spiritDispenserControl.SpiritDispenserSetting);
        }

        [HttpGet("status")]
        public ActionResult<SpiritDispenserPosition> Status()
        {
            return new ObjectResult(_spiritDispenserControl.Status);
        }

        [HttpGet("current-position")]
        public ActionResult<SpiritDispenserPosition> CurrentPosition()
        {
            return new ObjectResult(_spiritDispenserControl.CurrentPosition);
        }

        [HttpPost("reference-drive")]
        public async Task<ActionResult> ReferenceDrive()
        {
            await _spiritDispenserControl.ReferenceDriveAsync();
            return new OkObjectResult(new OkResult());
        }

        [HttpPost("goto-bottle-change")]
        public async Task<ActionResult> GoToBottleChange()
        {
            await _spiritDispenserControl.GoToBottleChangePosition();
            return new OkObjectResult(new OkResult());
        }

        [HttpPost("fill-glas")]
        public async Task<ActionResult> FillGlas()
        {
            await _spiritDispenserControl.FillGlas();
            return new OkObjectResult(new OkResult());
        }

        [HttpPost("goto-home-position")]
        public async Task<ActionResult> GoToHomePosition()
        {
            await _spiritDispenserControl.CloseSpiritSpender();
            return new OkObjectResult(new OkResult());
        }

        [HttpPost("goto-release-position")]
        public async Task<ActionResult> GoToReleasePosition()
        {
            await _spiritDispenserControl.ReleaseSpirit();
            return new OkObjectResult(new OkResult());
        }
    }
}
