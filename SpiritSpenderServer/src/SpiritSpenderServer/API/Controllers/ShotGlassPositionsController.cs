﻿namespace SpiritSpenderServer.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using SpiritSpenderServer.Automatic;
using SpiritSpenderServer.Persistence.Positions;

[Route("api/[controller]")]
[ApiController]
public class ShotGlassPositionsController : ControllerBase
{
    private readonly IShotGlassPositionSettingRepository _shotGlassPositionSettingRepository;
    private readonly IAutomaticMode _automaticMode;

    public ShotGlassPositionsController(IShotGlassPositionSettingRepository shotGlassPositionSettingRepository, IAutomaticMode automaticMode)
        => (_shotGlassPositionSettingRepository, _automaticMode) =
        (shotGlassPositionSettingRepository, automaticMode);

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShotGlassPositionSetting>>> Get()
    {
        var result = await _shotGlassPositionSettingRepository.GetAllSettingsAsync();
        return new ObjectResult(result);
    }

    [HttpGet("count")]
    public async Task<ActionResult<long>> GetNumberOfPositions()
    {
        var numberOfPositions = await _shotGlassPositionSettingRepository.GetCountAsync();

        return new ObjectResult(numberOfPositions);
    }

    [HttpPut("clear")]
    public async Task<ActionResult<IEnumerable<ShotGlassPositionSetting>>> PutClearPositions()
    {
        var result = await _shotGlassPositionSettingRepository.ClearQuantityAsync();
        if (result == false)
            return new NotFoundResult();

        return new ObjectResult(await _shotGlassPositionSettingRepository.GetAllSettingsAsync());
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

        if (positionSetting == null || positionSetting.Position == null)
            return new NotFoundResult();

        await _automaticMode.DriveToPositionAsync(positionSetting.Position);

        return new OkObjectResult(new OkResult());
    }
}