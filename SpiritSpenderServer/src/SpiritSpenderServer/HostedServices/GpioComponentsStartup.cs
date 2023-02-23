namespace SpiritSpenderServer.HostedServices;

using NC_Communication;
using SpiritSpenderServer.Config.HardwareConfiguration;
using SpiritSpenderServer.HardwareControl.SpiritSpenderControl;
using SpiritSpenderServer.HardwareControl.StatusLamp;

public class GpioComponentsStartup : IHostedService
{
    private readonly ISpiritDispenserControl _spiritDispenserControl;
    private readonly IStatusLamp _statusLamp;
    private readonly INcCommunication _ncCommunication;
    private readonly IShotGlassPositionSettingsConfiguration _shotGlassPositionSettingsConfiguration;

    public GpioComponentsStartup(
        ISpiritDispenserControl spiritDispenserControl,
        IStatusLamp statusLamp,
        INcCommunication ncCommunication,
        IShotGlassPositionSettingsConfiguration shotGlassPositionSettingsConfiguration)
    {
        _spiritDispenserControl = spiritDispenserControl;
        _statusLamp = statusLamp;
        _ncCommunication = ncCommunication;
        _shotGlassPositionSettingsConfiguration = shotGlassPositionSettingsConfiguration;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _shotGlassPositionSettingsConfiguration.InitShotGlassPositionSettingsAsync();
        await _ncCommunication.InitAsync();
        await _spiritDispenserControl.InitAsync();
        await _statusLamp.InitAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
