using SpiritSpenderServer.Config.HardwareConfiguration;
using SpiritSpenderServer.HardwareControl.SpiritSpenderMotor;
using SpiritSpenderServer.HardwareControl.StepperDrive;
using SpiritSpenderServer.Persistence.Positions;
using System.Linq;
using System.Threading.Tasks;

namespace SpiritSpenderServer.Automatic
{
    public class AutomaticMode : IAutomaticMode
    {
        const string X_AXIS_NAME = "X";
        const string Y_AXIS_NAME = "Y";
        private readonly IShotGlassPositionSettingRepository _shotGlassPositionSettingRepository;
        private readonly ISpiritDispenserControl _spiritDispenserControl;
        private readonly IStepperDrive _X_Axis;
        private readonly IStepperDrive _Y_Axis;
        
        public AutomaticMode(IShotGlassPositionSettingRepository shotGlassPositionSettingRepository, IHardwareConfiguration hardwareConfiguration)
        {
            _shotGlassPositionSettingRepository = shotGlassPositionSettingRepository;
            _X_Axis = hardwareConfiguration.StepperDrives[X_AXIS_NAME];
            _Y_Axis = hardwareConfiguration.StepperDrives[Y_AXIS_NAME];
            _spiritDispenserControl = hardwareConfiguration.SpiritDispenserControl;
        }

        public async Task ReleaseTheSpiritAsync()
        {
            var positionSettings = await _shotGlassPositionSettingRepository.GetAllSettingsAsync();
            var orderedPositionSettings = positionSettings.ToList().OrderBy(ps => ps.Number);

            foreach (var positionSetting in orderedPositionSettings)
            {
                switch (positionSetting.Quantity)
                {
                    case Quantity.OneShot:
                        await DriveToPositionAsync(positionSetting.Position);
                        await _spiritDispenserControl.FillGlas();
                        break;
                    case Quantity.DoubleShot:
                        await DriveToPositionAsync(positionSetting.Position);
                        await _spiritDispenserControl.FillGlas();
                        await _spiritDispenserControl.FillGlas();
                        break;
                    case Quantity.Empty:
                        // do nothing
                        break;
                }
            }
        }

        public async Task DriveToPositionAsync(Position position)
        {
            var taskX = _X_Axis.DriveToPositionAsync(position.X);
            var taskY = _Y_Axis.DriveToPositionAsync(position.Y);
            
            await taskX;
            await taskY;
        }
    }
}
