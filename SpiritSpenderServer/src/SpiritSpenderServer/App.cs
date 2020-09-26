using SpiritSpenderServer.Config.HardwareConfiguration;
using SpiritSpenderServer.HardwareControl;
using SpiritSpenderServer.HardwareControl.Axis;
using SpiritSpenderServer.HardwareControl.EmergencyStop;
using SpiritSpenderServer.HardwareControl.SpiritSpenderMotor;

namespace SpiritSpenderServer
{
    public class App
    {
        public App(
            StatusObserver statusObserver,
            IXAxis xAxis,
            IYAxis yAxis,
            ISpiritDispenserControl spiritDispenserControl,
            IStatusLamp statusLamp,
            IShotGlassPositionSettingsConfiguration shotGlassPositionSettingsConfiguration)
        {
            shotGlassPositionSettingsConfiguration.CreateShotGlassPositionSettings().Wait();
            xAxis.InitAsync().Wait();
            yAxis.InitAsync().Wait();
            spiritDispenserControl.InitAsync().Wait();
            statusLamp.InitAsync().Wait();
            statusObserver.Init();
        }
    }
}
