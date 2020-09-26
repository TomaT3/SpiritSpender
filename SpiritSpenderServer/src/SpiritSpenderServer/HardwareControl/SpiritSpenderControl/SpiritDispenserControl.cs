using SpiritSpenderServer.HardwareControl.EmergencyStop;
using SpiritSpenderServer.Helper;
using SpiritSpenderServer.Persistence.SpiritDispenserSettings;
using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using UnitsNet;
using UnitsNet.Units;

namespace SpiritSpenderServer.HardwareControl.SpiritSpenderMotor
{
    public class SpiritDispenserControl : ISpiritDispenserControl
    {
        private string _name;
        private readonly BehaviorSubject<Status> _currentStatus;
        private ISpiritDispenserSettingRepository _spiritDispenserSettingRepository;
        private ILinearMotor _spiritSpenderMotor;
        private IEmergencyStop _emergencyStop;
        private AutoResetEvent _waitHandleSpiritDispenserRefilled = new AutoResetEvent(true);
        private System.Timers.Timer _spiritDispenserRefilledTimer;
        private CancellationTokenSource _cancelMovementTokensource;

        public SpiritDispenserControl(ISpiritDispenserSettingRepository dispenserSettingRepository, IEmergencyStop emergencyStop, IGpioControllerFacade gpioControllerFacade)
        {
            _name = "SpiritDispenser";
            (_spiritDispenserSettingRepository, _emergencyStop) = (dispenserSettingRepository, emergencyStop);
            
            _spiritSpenderMotor = new LinearMotor(forwardGpioPin: 18, backwardGpioPin: 23, gpioControllerFacade: gpioControllerFacade);

            _currentStatus = new BehaviorSubject<Status>(emergencyStop.EmergencyStopPressed ? Status.Error : Status.NotReady);
            CurrentPosition = SpiritDispenserPosition.Undefined;
            _cancelMovementTokensource = new CancellationTokenSource();
            emergencyStop.EmergencyStopPressedChanged += EmergencyStopPressedChanged;
        }

        public SpiritDispenserSetting SpiritDispenserSetting { get; private set; }
        
        public SpiritDispenserPosition CurrentPosition { get; private set; }

        public async Task InitAsync()
        {
            SpiritDispenserSetting = await _spiritDispenserSettingRepository.GetSpiritDispenserSetting(_name);
            if (SpiritDispenserSetting == null)
            {
                SpiritDispenserSetting = new SpiritDispenserSetting
                {
                    Name = _name,
                    DriveTimeFromBottleChangeToHomePos = new Duration(1.4, DurationUnit.Second),
                    DriveTimeFromHomePosToBottleChange = new Duration(2, DurationUnit.Second),
                    DriveTimeFromReleaseToHomePosition = new Duration(1, DurationUnit.Second),
                    DriveTimeFromHomeToReleasePosition = new Duration(1.5, DurationUnit.Second),
                    WaitTimeUntilSpiritIsReleased = new Duration(1.8, DurationUnit.Second),
                    WaitTimeUntilSpiritIsRefilled = new Duration(1.5, DurationUnit.Second)
                };

                await _spiritDispenserSettingRepository.Create(SpiritDispenserSetting);
            }
        }

        public IObservable<Status> GetStatusObservable() => _currentStatus.AsObservable();

        public async Task UpdateSettingsAsync(SpiritDispenserSetting setting)
        {
            await _spiritDispenserSettingRepository.Update(setting);
            SpiritDispenserSetting = await _spiritDispenserSettingRepository.GetSpiritDispenserSetting(_name);
        }

        public async Task ReferenceDriveAsync()
        {
            if (_emergencyStop.EmergencyStopPressed)
                return;

            CurrentPosition = SpiritDispenserPosition.Undefined;
            _currentStatus.OnNext(Status.NotReady);

            var timeToMoveMotorCompletelyUp = SpiritDispenserSetting.DriveTimeFromReleaseToHomePosition + SpiritDispenserSetting.DriveTimeFromHomePosToBottleChange;
            var drivingSuccessfully = await _spiritSpenderMotor.DriveForwardAsync(timeToMoveMotorCompletelyUp, _cancelMovementTokensource.Token);
            CheckDrivingResult(drivingSuccessfully, SpiritDispenserPosition.BottleChange);
        }

        public async Task FillGlas()
        {
            if (_emergencyStop.EmergencyStopPressed)
                return;

            _waitHandleSpiritDispenserRefilled.WaitOne();

            await ReleaseSpirit();
            await Convert.ToInt32(SpiritDispenserSetting.WaitTimeUntilSpiritIsReleased.Milliseconds).DelayExceptionFree(_cancelMovementTokensource.Token);
            await CloseSpiritSpender();

            StartRefillTimer(SpiritDispenserSetting.WaitTimeUntilSpiritIsRefilled);
        }

        public async Task GoToBottleChangePosition()
        {
            if (_emergencyStop.EmergencyStopPressed)
                return;

            if (CurrentPosition == SpiritDispenserPosition.ReleaseSpirit)
                await FromReleaseToHomePosition();

            if (CurrentPosition == SpiritDispenserPosition.Home)
                await FromHomeToBottleChangePosition();
        }

        public async Task ReleaseSpirit()
        {
            if (_emergencyStop.EmergencyStopPressed)
                return;
            
            if (CurrentPosition == SpiritDispenserPosition.BottleChange)
                await FromBottleChangeToHomePosition();

            if(CurrentPosition== SpiritDispenserPosition.Home)
                await FromHomeToReleasePosition();
        }

        public async Task CloseSpiritSpender()
        {
            if (_emergencyStop.EmergencyStopPressed)
                return;

            if(CurrentPosition == SpiritDispenserPosition.ReleaseSpirit)
                await FromReleaseToHomePosition();

            if (CurrentPosition == SpiritDispenserPosition.BottleChange)
                await FromBottleChangeToHomePosition();
        }

        private async Task FromHomeToReleasePosition()
        {
            var drivingSuccessfully = await _spiritSpenderMotor.DriveBackwardAsync(SpiritDispenserSetting.DriveTimeFromHomeToReleasePosition, _cancelMovementTokensource.Token);
            CheckDrivingResult(drivingSuccessfully, SpiritDispenserPosition.ReleaseSpirit);
        }

        private async Task FromReleaseToHomePosition()
        {
            var drivingSuccessfully = await _spiritSpenderMotor.DriveForwardAsync(SpiritDispenserSetting.DriveTimeFromReleaseToHomePosition, _cancelMovementTokensource.Token);
            CheckDrivingResult(drivingSuccessfully, SpiritDispenserPosition.Home);
        }

        private async Task FromHomeToBottleChangePosition()
        {
            var drivingSuccessfully = await _spiritSpenderMotor.DriveForwardAsync(SpiritDispenserSetting.DriveTimeFromHomePosToBottleChange, _cancelMovementTokensource.Token);
            CheckDrivingResult(drivingSuccessfully, SpiritDispenserPosition.BottleChange);
        }

        private async Task FromBottleChangeToHomePosition()
        {
            var drivingSuccessfully = await _spiritSpenderMotor.DriveBackwardAsync(SpiritDispenserSetting.DriveTimeFromBottleChangeToHomePos, _cancelMovementTokensource.Token);
            CheckDrivingResult(drivingSuccessfully, SpiritDispenserPosition.Home);
        }

        private void CheckDrivingResult(bool drivingResult, SpiritDispenserPosition newPosition)
        {
            if(drivingResult)
            {
                _currentStatus.OnNext(Status.Ready);
                CurrentPosition = newPosition;
            }
            else
            {
                _currentStatus.OnNext(Status.NotReady);
                CurrentPosition = SpiritDispenserPosition.Undefined;
            }
        }

        private void EmergencyStopPressedChanged(bool emergencyStopPressed)
        {
            if (emergencyStopPressed)
                StopMovement();
        }

        private void StopMovement()
        {
            try
            {
                _cancelMovementTokensource.Cancel();
            }
            finally
            {
                _cancelMovementTokensource = new CancellationTokenSource();
            }

            _waitHandleSpiritDispenserRefilled.Set();
        }

        private void StartRefillTimer(Duration timeToRefill)
        {
            _spiritDispenserRefilledTimer = new System.Timers.Timer(timeToRefill.Milliseconds);
            _spiritDispenserRefilledTimer.AutoReset = false;
            _spiritDispenserRefilledTimer.Elapsed += (s, e) => { _waitHandleSpiritDispenserRefilled.Set(); };

            _spiritDispenserRefilledTimer.Start();
        }
    }
}
