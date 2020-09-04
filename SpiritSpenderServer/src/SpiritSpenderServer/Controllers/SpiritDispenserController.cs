using System.Threading.Tasks;
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
        private ISpiritDispenserSettingRepository _spiritDispenserSettingRepository;
        private IHardwareConfiguration _hardwareConfiguration;
        private string _name;
        public SpiritDispenserController(ISpiritDispenserSettingRepository spiritDispenserSettingRepository, IHardwareConfiguration hardwareConfiguration)
            => (_spiritDispenserSettingRepository, _hardwareConfiguration, _name) = (spiritDispenserSettingRepository, hardwareConfiguration, hardwareConfiguration.SpiritDispenserControl.Name);

        [HttpGet]
        public ActionResult<string> Get()
        {
            return new ObjectResult(_name);
        }

        [HttpGet("setting")]
        public async Task<ActionResult<SpiritDispenserSetting>> GetSetting()
        {
            var spiritDispenserSetting = await _spiritDispenserSettingRepository.GetSpiritDispenserSetting(_name);
            if (spiritDispenserSetting == null)
                return new NotFoundResult();

            return new ObjectResult(spiritDispenserSetting);
        }

        [HttpPut("setting")]
        public async Task<ActionResult<SpiritDispenserSetting>> Put([FromBody] SpiritDispenserSetting spiritDispenserSetting)
        {
            var sdsFromDb = await _spiritDispenserSettingRepository.GetSpiritDispenserSetting(_name);
            if (sdsFromDb == null)
                return new NotFoundResult();

            await _spiritDispenserSettingRepository.Update(spiritDispenserSetting);
            await _hardwareConfiguration.SpiritDispenserControl.UpdateSettingsAsync();
            return new OkObjectResult(spiritDispenserSetting);
        }

        [HttpGet("status")]
        public ActionResult<SpiritDispenserPosition> Status()
        {
            return new ObjectResult(_hardwareConfiguration.SpiritDispenserControl.Status);
        }

        [HttpGet("current-position")]
        public ActionResult<SpiritDispenserPosition> CurrentPosition()
        {
            return new ObjectResult(_hardwareConfiguration.SpiritDispenserControl.CurrentPosition);
        }

        [HttpPost("reference-drive")]
        public async Task<ActionResult> ReferenceDrive()
        {
            await _hardwareConfiguration.SpiritDispenserControl.ReferenceDriveAsync();
            return new OkObjectResult(new OkResult());
        }

        [HttpPost("goto-bottle-change")]
        public async Task<ActionResult> GoToBottleChange()
        {
            await _hardwareConfiguration.SpiritDispenserControl.GoToBottleChangePosition();
            return new OkObjectResult(new OkResult());
        }

        [HttpPost("fill-glas")]
        public async Task<ActionResult> FillGlas()
        {
            await _hardwareConfiguration.SpiritDispenserControl.FillGlas();
            return new OkObjectResult(new OkResult());
        }

        [HttpPost("goto-home-position")]
        public async Task<ActionResult> GoToHomePosition()
        {
            await _hardwareConfiguration.SpiritDispenserControl.CloseSpiritSpender();
            return new OkObjectResult(new OkResult());
        }

        [HttpPost("goto-release-position")]
        public async Task<ActionResult> GoToReleasePosition()
        {
            await _hardwareConfiguration.SpiritDispenserControl.ReleaseSpirit();
            return new OkObjectResult(new OkResult());
        }
    }
}
