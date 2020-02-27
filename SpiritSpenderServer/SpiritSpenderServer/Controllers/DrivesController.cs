using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpiritSpenderServer.Config.HardwareConfiguration;
using SpiritSpenderServer.Persistence.DriveSettings;
using UnitsNet;

namespace SpiritSpenderServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrivesController : ControllerBase
    {
        private readonly IDriveSettingRepository _driveSettingsRepo;
        private readonly IHardwareConfiguration _hardwareConfiguration;

        public DrivesController(IDriveSettingRepository driveSettingRepository, IHardwareConfiguration hardwareConfiguration)
            => (_driveSettingsRepo, _hardwareConfiguration) = (driveSettingRepository, hardwareConfiguration);
       

        [HttpGet]
        public async Task<ActionResult<DriveSetting>> Get()
        {
            return new ObjectResult(await _driveSettingsRepo.GetAllDriveSettingNames());
        }

        [HttpGet("{driveName}/setting")]
        public async Task<ActionResult<DriveSetting>> GetSetting(string driveName)
        {
            var driveSetting = await _driveSettingsRepo.GetDriveSetting(driveName);
            if (driveSetting == null)
                return new NotFoundResult();


            return new ObjectResult(driveSetting);
        }

        [HttpPut("{driveName}/setting")]
        public async Task<ActionResult<DriveSetting>> Put(string driveName, [FromBody] DriveSetting driveSetting)
        {
            var dsFromDb = await _driveSettingsRepo.GetDriveSetting(driveName);
            if (dsFromDb == null)
                return new NotFoundResult();

            await _driveSettingsRepo.Update(driveSetting);
            await _hardwareConfiguration.StepperDrives[driveName].UpdateSettingsAsync();
            return new OkObjectResult(driveSetting);
        }

        [HttpGet("{driveName}/currentposition")]
        public ActionResult<Length> CurrentPosition(string driveName)
        {
            return new ObjectResult(_hardwareConfiguration.StepperDrives[driveName].CurrentPosition);
        }

        [HttpPost("{driveName}/drivetoposition")]
        public ActionResult DriveToPosition(string driveName, [FromBody] Length position)
        {
            _hardwareConfiguration.StepperDrives[driveName].DriveToPosition(position);
            return new OkObjectResult(new OkResult());
        }

        [HttpPost("{driveName}/drivedistance")]
        public ActionResult DriveDistance(string driveName, [FromBody] Length distance)
        {
            _hardwareConfiguration.StepperDrives[driveName].DriveDistance(distance);
            return new OkObjectResult(new OkResult());
        }

        [HttpPost("{driveName}/setposition")]
        public ActionResult SetPosition(string driveName, [FromBody] Length position)
        {
            _hardwareConfiguration.StepperDrives[driveName].SetPosition(position);
            return new OkObjectResult(new OkResult());
        }
    }
}
