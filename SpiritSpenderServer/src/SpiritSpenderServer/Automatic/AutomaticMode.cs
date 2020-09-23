using SpiritSpenderServer.Config.HardwareConfiguration;
using SpiritSpenderServer.HardwareControl;
using SpiritSpenderServer.HardwareControl.EmergencyStop;
using SpiritSpenderServer.HardwareControl.SpiritSpenderMotor;
using SpiritSpenderServer.HardwareControl.StepperDrive;
using SpiritSpenderServer.Persistence.Positions;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace SpiritSpenderServer.Automatic
{
    public class AutomaticMode : IAutomaticMode
    {
        const string X_AXIS_NAME = "X";
        const string Y_AXIS_NAME = "Y";
        private readonly BehaviorSubject<Status> _currentStatus;
        private readonly IShotGlassPositionSettingRepository _shotGlassPositionSettingRepository;
        private readonly IEmergencyStop _emergencyStop;
        private readonly ISpiritDispenserControl _spiritDispenserControl;
        private readonly IAxis _X_Axis;
        private readonly IAxis _Y_Axis;

        public AutomaticMode(IShotGlassPositionSettingRepository shotGlassPositionSettingRepository, IHardwareConfiguration hardwareConfiguration)
        {
            _currentStatus = new BehaviorSubject<Status>(Status.NotReady);
            _shotGlassPositionSettingRepository = shotGlassPositionSettingRepository;
            _X_Axis = hardwareConfiguration.StepperDrives[X_AXIS_NAME];
            _Y_Axis = hardwareConfiguration.StepperDrives[Y_AXIS_NAME];
            _spiritDispenserControl = hardwareConfiguration.SpiritDispenserControl;
            _emergencyStop = hardwareConfiguration.EmergencyStop;
            _emergencyStop.EmergencyStopPressedChanged += (estop) => CalculateStatuts();

            _X_Axis.GetStatusObservable()
                .Merge(_Y_Axis.GetStatusObservable())
                .Merge(_spiritDispenserControl.GetStatusObservable())
                .Do(_ => CalculateStatuts());
        }

        public IObservable<Status> GetStatusObservable() => _currentStatus.AsObservable();

        public async Task ReleaseTheSpiritAsync()
        {
            var positionSettings = await _shotGlassPositionSettingRepository.GetAllSettingsAsync();
            var orderedPositionSettings = positionSettings.ToList().OrderBy(ps => ps.Number);

            foreach (var positionSetting in orderedPositionSettings)
            {
                if (_currentStatus.Value != Status.Ready)
                {
                    break;
                }

                _currentStatus.OnNext(Status.Running);

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

            CalculateStatuts(true);
        }

        public async Task DriveToPositionAsync(Position position)
        {
            var taskX = _X_Axis.DriveToPositionAsync(position.X);
            var taskY = _Y_Axis.DriveToPositionAsync(position.Y);
            
            await taskX;
            await taskY;
        }

        private void CalculateStatuts(bool resetState = false)
        {
            if (_currentStatus.Value != Status.Running || resetState)
            {
                if (_emergencyStop.EmergencyStopPressed)
                    _currentStatus.OnNext(Status.Error);

                if (IsStartPossible())
                    _currentStatus.OnNext(Status.Ready);
                else
                    _currentStatus.OnNext(Status.NotReady);
            }
        }

        private bool IsStartPossible()
        {
            var startPossible = !_emergencyStop.EmergencyStopPressed &&
                                _spiritDispenserControl.GetStatusObservable().LastAsync().Wait() == Status.Ready &&
                                _X_Axis.GetStatusObservable().LastAsync().Wait() == Status.Ready &&
                                _Y_Axis.GetStatusObservable().LastAsync().Wait() == Status.Ready;

            return startPossible;
        }
    }
}
