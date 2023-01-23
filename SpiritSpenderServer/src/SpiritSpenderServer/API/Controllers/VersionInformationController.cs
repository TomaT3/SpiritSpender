using Microsoft.AspNetCore.Mvc;
using System;

namespace SpiritSpenderServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionInformationController : ControllerBase
    {
        [HttpGet]
        public ActionResult<VersionInfo> Get()
        {
            const string releaseNotesUrl = "https://github.com/TomaT3/SpiritSpender/releases";
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            var versionString = version?.ToString(3);
            var versionInfo = new VersionInfo() { Version = versionString ?? "no version detected", ReleaseNotesUrl = releaseNotesUrl };
            return new ObjectResult(versionInfo);
        }
    }

    public record VersionInfo
    {
        public string? Version { get; set; }
        public string? ReleaseNotesUrl { get; set; }
    }
}
