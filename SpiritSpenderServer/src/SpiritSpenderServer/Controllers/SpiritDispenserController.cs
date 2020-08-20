using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpiritSpenderServer.Config.HardwareConfiguration;
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

        [HttpPost("fill-glas")]
        public async Task<ActionResult> FillGlas()
        {
            await _hardwareConfiguration.SpiritDispenserControl.FillGlas();
            return new OkObjectResult(new OkResult());
        }

        [HttpPost("close-spirit-spender")]
        public async Task<ActionResult> CloseSpiritSpender()
        {
            await _hardwareConfiguration.SpiritDispenserControl.CloseSpiritSpender();
            return new OkObjectResult(new OkResult());
        }

        [HttpPost("release-spirit")]
        public async Task<ActionResult> ReleaseSpirit()
        {
            await _hardwareConfiguration.SpiritDispenserControl.ReleaseSpirit();
            return new OkObjectResult(new OkResult());
        }
    }
}
