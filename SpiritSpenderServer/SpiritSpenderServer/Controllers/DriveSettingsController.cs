using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpiritSpenderServer.Persistence;

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

        // GET: api/DriveSettings/5
        [HttpGet("{driveName}")]
        public async Task<ActionResult<DriveSetting>> Get(string driveName)
        {
            var todo = await _driveSettingsRepo.GetDriveSetting(driveName);
            if (todo == null)
                return new NotFoundResult();

            return new ObjectResult(todo);
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
            driveSetting.InternalId = todoFromDb.InternalId;
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
