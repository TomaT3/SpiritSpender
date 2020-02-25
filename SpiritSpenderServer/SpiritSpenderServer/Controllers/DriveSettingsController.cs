using Microsoft.AspNetCore.Mvc;
using SpiritSpenderServer.Persistence.DriveSettings;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitsNet;
using UnitsNet.Units;

namespace SpiritSpenderServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriveSettingsController : ControllerBase
    {
        private readonly IDriveSettingRepository _driveSettingsRepo;

        public DriveSettingsController(IDriveSettingRepository driveSettingRepository)
        {
            _driveSettingsRepo = driveSettingRepository;
        }

        // GET: api/DriveSettings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriveSetting>>> Get()
        {
            return new ObjectResult(await _driveSettingsRepo.GetAllDriveSettings());
        }

        [HttpGet("test")]
        public async Task<ActionResult<DriveSetting>> GetTest()
        {
            var test = new DriveSetting
            {
                DriveName = "X",
                Acceleration = new Acceleration(20, AccelerationUnit.MillimeterPerSecondSquared),
                MaxSpeed = new Speed(200, SpeedUnit.MillimeterPerSecond),
                SpindelPitch = new Length(8, LengthUnit.Millimeter),
                StepsPerRevolution = 400,
                ReverseDirection = false
            };
            await _driveSettingsRepo.Create(test);
            return new ObjectResult(test);
        }

        // GET: api/DriveSettings/5
        [HttpGet("{driveName}")]
        public async Task<ActionResult<DriveSetting>> Get(string driveName)
        {
            var driveSettings = await _driveSettingsRepo.GetDriveSetting(driveName);
            if (driveSettings == null)
                return new NotFoundResult();

            
            return new ObjectResult(driveSettings);
        }

        // POST: api/DriveSettings
        [HttpPost]
        public async Task<ActionResult<DriveSetting>> Post([FromBody] DriveSetting driveSetting)
        {
            await _driveSettingsRepo.Create(driveSetting);
            return new OkObjectResult(driveSetting);
        }

        // PUT: api/DriveSettings/5
        [HttpPut("{driveName}")]
        public async Task<ActionResult<DriveSetting>> Put(string driveName, [FromBody] DriveSetting driveSetting)
        {
            var todoFromDb = await _driveSettingsRepo.GetDriveSetting(driveName);
            if (todoFromDb == null)
                return new NotFoundResult();
            driveSetting.DriveName = todoFromDb.DriveName;
            //driveSetting.InternalId = todoFromDb.InternalId;
            await _driveSettingsRepo.Update(driveSetting);
            return new OkObjectResult(driveSetting);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{driveName}")]
        public async Task<IActionResult> Delete(string driveName)
        {
            var driveToDelete = await _driveSettingsRepo.GetDriveSetting(driveName);
            if (driveToDelete == null)
                return new NotFoundResult();
            await _driveSettingsRepo.Delete(driveName);
            return new OkResult();
        }
    }
}
