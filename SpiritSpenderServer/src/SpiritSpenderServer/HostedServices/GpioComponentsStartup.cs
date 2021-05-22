using Microsoft.Extensions.Hosting;
using SpiritSpenderServer.Config.HardwareConfiguration;
using SpiritSpenderServer.HardwareControl;
using SpiritSpenderServer.HardwareControl.Axis;
using SpiritSpenderServer.HardwareControl.EmergencyStop;
using SpiritSpenderServer.HardwareControl.SpiritSpenderMotor;
using System.Threading;
using System.Threading.Tasks;

namespace SpiritSpenderServer.HostedServices
{
    public class GpioComponentsStartup : IHostedService
    {
        private readonly IXAxis _xAxis;
        private readonly IYAxis _yAxis;
        private readonly ISpiritDispenserControl _spiritDispenserControl;
        private readonly IStatusLamp _statusLamp;
        private readonly IShotGlassPositionSettingsConfiguration _shotGlassPositionSettingsConfiguration;

        public GpioComponentsStartup(
            IXAxis xAxis,
            IYAxis yAxis,
            ISpiritDispenserControl spiritDispenserControl,
            IStatusLamp statusLamp,
            IShotGlassPositionSettingsConfiguration shotGlassPositionSettingsConfiguration)
        {
            _xAxis = xAxis;
            _yAxis = yAxis;
            _spiritDispenserControl = spiritDispenserControl;
            _statusLamp = statusLamp;
            _shotGlassPositionSettingsConfiguration = shotGlassPositionSettingsConfiguration;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _shotGlassPositionSettingsConfiguration.InitShotGlassPositionSettingsAsync();
            await _xAxis.InitAsync();
            await _yAxis.InitAsync();
            await _spiritDispenserControl.InitAsync();
            await _statusLamp.InitAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
