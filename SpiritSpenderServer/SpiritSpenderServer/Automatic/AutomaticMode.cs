using SpiritSpenderServer.Config.HardwareConfiguration;
using SpiritSpenderServer.HardwareControl.SpiritSpenderMotor;
using SpiritSpenderServer.HardwareControl.StepperDrive;
using SpiritSpenderServer.Persistence.Positions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
           => (_shotGlassPositionSettingRepository, _X_Axis, _Y_Axis, _spiritDispenserControl) =
           (shotGlassPositionSettingRepository, hardwareConfiguration.StepperDrives[X_AXIS_NAME], hardwareConfiguration.StepperDrives[Y_AXIS_NAME], hardwareConfiguration.SpiritDispenserControl);


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
                        _spiritDispenserControl.FillGlas();
                        // ToDo: add setting with wait time until spirit dispenser is refilled and start timer
                        break;
                    case Quantity.DoubleShot:
                        await DriveToPositionAsync(positionSetting.Position);
                        _spiritDispenserControl.FillGlas();
                        Thread.Sleep(2000); // ToDo: add setting with wait time until spirit dispenser is refilled and start timer
                        _spiritDispenserControl.FillGlas();
                        break;
                    case Quantity.Empty:
                        // do nothing
                        break;
                }
            }
        }

        public Task DriveToPositionAsync(Position position)
        {
            var task = Task.Run(() =>
            {
                _X_Axis.DriveToPosition(position.X);
                _Y_Axis.DriveToPosition(position.Y);
            });

            return task;
        }
    }
}
