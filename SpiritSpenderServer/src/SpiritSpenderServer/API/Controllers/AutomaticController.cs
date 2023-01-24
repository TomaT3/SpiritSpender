namespace SpiritSpenderServer.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using SpiritSpenderServer.Automatic;

[Route("api/[controller]")]
[ApiController]
public class AutomaticController : ControllerBase
{
    private readonly IAutomaticMode _automaticMode;

    public AutomaticController(IAutomaticMode automaticMode)
        => _automaticMode = automaticMode;

    [HttpPost("release-the-spirit")]
    public async Task<ActionResult> DriveToPosition()
    {
        await _automaticMode.ReleaseTheSpiritAsync();
        return new OkObjectResult(new OkResult());
    }

    [HttpPost("reference-all-axis")]
    public async Task<ActionResult> ReferenceAllAxis()
    {
        await _automaticMode.ReferenceAllAxis();
        return new OkObjectResult(new OkResult());
    }

    [HttpPost("go-to-bottle-change-position")]
    public async Task<ActionResult> GoToBottleChangePosition()
    {
        await _automaticMode.GoToBottleChange();
        return new OkObjectResult(new OkResult());
    }
}