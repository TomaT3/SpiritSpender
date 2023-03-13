namespace SpiritSpenderServer.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using Model.NcController;
using NC_Communication;
using SpiritSpenderServer.Automatic;

[Route("api/[controller]")]
[ApiController]
public class NcController : ControllerBase
{
    private readonly INcCommunication _ncCommunication;

    public NcController(INcCommunication ncCommunication)
    {
        _ncCommunication = ncCommunication;
    }

    [HttpPost("send-command")]
    public ActionResult SendCommand(NcCommand ncCommand)
    {
        _ncCommunication.SendCommand(ncCommand.Command);
        return new OkObjectResult(new OkResult());
    }

}