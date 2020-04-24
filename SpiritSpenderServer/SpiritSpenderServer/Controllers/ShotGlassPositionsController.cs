using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpiritSpenderServer.Config.HardwareConfiguration;
using SpiritSpenderServer.HardwareControl.StepperDrive;
using SpiritSpenderServer.Persistence.Positions;

namespace SpiritSpenderServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShotGlassPositionsController : ControllerBase
    {
        const string X_AXIS_NAME = "X";
        const string Y_AXIS_NAME = "Y";
        private readonly IShotGlassPositionSettingRepository _shotGlassPositionSettingRepository;
        private readonly IStepperDrive _X_Axis;
        private readonly IStepperDrive _Y_Axis;


        public ShotGlassPositionsController(IShotGlassPositionSettingRepository shotGlassPositionSettingRepository, IHardwareConfiguration hardwareConfiguration)
            => (_shotGlassPositionSettingRepository, _X_Axis, _Y_Axis) = 
            (shotGlassPositionSettingRepository, hardwareConfiguration.StepperDrives[X_AXIS_NAME], hardwareConfiguration.StepperDrives[Y_AXIS_NAME]);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShotGlassPositionSetting>>> Get()
        {
            return new ObjectResult(await _shotGlassPositionSettingRepository.GetAllSettingsAsync());
        }

        [HttpGet("count")]
        public async Task<ActionResult<long>> GetNumberOfPositions()
        {
            var numberOfPositions = await _shotGlassPositionSettingRepository.GetCountAsync();

            return new ObjectResult(numberOfPositions);
        }

        [HttpGet("{positionNumber}/setting")]
        public async Task<ActionResult<ShotGlassPositionSetting>> GetSetting(int positionNumber)
        {
            var positionSetting = await _shotGlassPositionSettingRepository.GetSettingAsync(positionNumber);
            if (positionSetting == null)
                return new NotFoundResult();

            return new ObjectResult(positionSetting);
        }

        [HttpGet("{positionNumber}/position")]
        public async Task<ActionResult<Position>> GetPosition(int positionNumber)
        {
            var positionSetting = await _shotGlassPositionSettingRepository.GetSettingAsync(positionNumber);
            if (positionSetting == null)
                return new NotFoundResult();

            return new ObjectResult(positionSetting.Position);
        }

        [HttpGet("{positionNumber}/quantity")]
        public async Task<ActionResult<Quantity>> GetQuantity(int positionNumber)
        {
            var positionSetting = await _shotGlassPositionSettingRepository.GetSettingAsync(positionNumber);
            if (positionSetting == null)
                return new NotFoundResult();

            return new ObjectResult(positionSetting.Quantity);
        }

        [HttpPut("{positionNumber}/position")]
        public async Task<ActionResult<Position>> PutPosition(int positionNumber, [FromBody] Position position)
        {
            var result = await _shotGlassPositionSettingRepository.UpdatePositionAsync(positionNumber, position);
            if (result == false)
                return new NotFoundResult();

            return new ObjectResult(position);
        }

        [HttpPut("{positionNumber}/quantity")]
        public async Task<ActionResult<Quantity>> PutQuantity(int positionNumber, [FromBody] Quantity quantity)
        {
            var result = await _shotGlassPositionSettingRepository.UpdateQuantityAsync(positionNumber, quantity);
            if (result == false)
                return new NotFoundResult();

            return new ObjectResult(quantity);
        }

        [HttpPost("{positionNumber}/drive-to-position")]
        public async Task<ActionResult> DriveToPosition(int positionNumber)
        {
            var positionSetting = await _shotGlassPositionSettingRepository.GetSettingAsync(positionNumber);

            await DriveToPosition(positionSetting.Position);

            return new OkObjectResult(new OkResult());
        }

        private Task DriveToPosition(Position position)
        {
            var task = Task.Run(() => {
                _X_Axis.DriveToPosition(position.X);
                _Y_Axis.DriveToPosition(position.Y);
            });

            return task;
        }
    }
}