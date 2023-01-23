using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpiritSpenderServer.Config.HardwareConfiguration;
using SpiritSpenderServer.HardwareControl.Axis;
using SpiritSpenderServer.Persistence.DriveSettings;
using UnitsNet;

namespace SpiritSpenderServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrivesController : ControllerBase
    {
        private readonly Dictionary<string, IAxis> _axis;

        public DrivesController(IXAxis xAxis, IYAxis yAxis)
        {
            _axis = new Dictionary<string, IAxis>()
            {
                { xAxis.Name, xAxis },
                { yAxis.Name, yAxis }
            };
            
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new ObjectResult(_axis.Keys);
        }

        [HttpGet("{driveName}/setting")]
        public ActionResult<DriveSetting> GetSetting(string driveName)
        {
            if (!_axis.TryGetValue(driveName, out var selectedAxis))
                return new NotFoundResult();

            return new ObjectResult(selectedAxis.DriveSetting);
        }

        [HttpPut("{driveName}/setting")]
        public async Task<ActionResult<DriveSetting>> Put(string driveName, [FromBody] DriveSetting driveSetting)
        {
            if (!_axis.TryGetValue(driveName, out var selectedAxis))
                return new NotFoundResult();

            await selectedAxis.UpdateSettingsAsync(driveSetting);
            return new OkObjectResult(driveSetting);
        }

        [HttpGet("{driveName}/current-position")]
        public ActionResult<Length> CurrentPosition(string driveName)
        {
            return new ObjectResult(_axis[driveName].CurrentPosition);
        }

        [HttpPost("{driveName}/drive-to-position")]
        public ActionResult DriveToPosition(string driveName, [FromBody] Length position)
        {
            _axis[driveName].DriveToPositionAsync(position);
            return new OkObjectResult(new OkResult());
        }

        [HttpPost("{driveName}/drive-distance")]
        public ActionResult DriveDistance(string driveName, [FromBody] Length distance)
        {
            _axis[driveName].DriveDistanceAsync(distance);
            return new OkObjectResult(new OkResult());
        }

        [HttpPost("{driveName}/set-position")]
        public ActionResult SetPosition(string driveName, [FromBody] Length position)
        {
            _axis[driveName].SetPosition(position);
            return new OkObjectResult(new OkResult());
        }

        [HttpPost("{driveName}/reference-drive")]
        public ActionResult ReferenceDrive(string driveName)
        {
            _axis[driveName].ReferenceDriveAsync();
            return new OkObjectResult(new OkResult());
        }
    }
}
