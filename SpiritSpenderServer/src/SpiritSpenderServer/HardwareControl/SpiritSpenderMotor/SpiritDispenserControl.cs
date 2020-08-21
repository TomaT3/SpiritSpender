using SpiritSpenderServer.HardwareControl.EmergencyStop;
using SpiritSpenderServer.Persistence.SpiritDispenserSettings;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnitsNet;

namespace SpiritSpenderServer.HardwareControl.SpiritSpenderMotor
{
    public class SpiritDispenserControl : ISpiritDispenserControl
    {
        private string _name;
        private ISpiritDispenserSettingRepository _spiritDispenserSettingRepository;
        private SpiritDispenserSetting _spiritDispenserSetting;
        private ILinearMotor _spiritSpenderMotor;
        private IEmergencyStop _emergencyStop;
        private AutoResetEvent _waitHandleSpiritDispenserRefilled = new AutoResetEvent(true);
        private System.Timers.Timer _spiritDispenserRefilledTimer;
        private CancellationTokenSource _cancelMovementTokensource;
        private object _lockObject = new Object();

        public SpiritDispenserControl(ILinearMotor spiritSpenderMotor, ISpiritDispenserSettingRepository dispenserSettingRepository, IEmergencyStop emergencyStop, string name)
        {
            (_spiritSpenderMotor, _spiritDispenserSettingRepository, _emergencyStop, _name) =
                (spiritSpenderMotor, dispenserSettingRepository, emergencyStop, name);

            _cancelMovementTokensource = new CancellationTokenSource();
            emergencyStop.EmergencyStopPressedChanged += EmergencyStopPressedChanged;
        }

        public string Name => _name;

        public async Task UpdateSettingsAsync()
        {
            _spiritDispenserSetting = await _spiritDispenserSettingRepository.GetSpiritDispenserSetting(_name);
        }

        public async Task FillGlas()
        {
            if (_emergencyStop.EmergencyStopPressed)
                return;

            _waitHandleSpiritDispenserRefilled.WaitOne();

            await ReleaseSpirit();
            await Task.Delay(Convert.ToInt32(_spiritDispenserSetting.WaitTimeUntilSpiritIsReleased.Milliseconds), _cancelMovementTokensource.Token);
            await CloseSpiritSpender();

            StartRefillTimer(_spiritDispenserSetting.WaitTimeUntilSpiritIsRefilled);
        }

        public async Task ReleaseSpirit()
        {
            if (_emergencyStop.EmergencyStopPressed)
                return;

            await _spiritSpenderMotor.DriveBackwardAsync(_spiritDispenserSetting.DriveTimeToReleaseTheSpirit, _cancelMovementTokensource.Token);
        }

        public async Task CloseSpiritSpender()
        {
            if (_emergencyStop.EmergencyStopPressed)
                return;

            await _spiritSpenderMotor.DriveForwardAsync(_spiritDispenserSetting.DriveTimeToCloseTheSpiritSpender, _cancelMovementTokensource.Token);
        }

        private void EmergencyStopPressedChanged(bool emergencyStopPressed)
        {
            if (emergencyStopPressed)
                StopMovement();
        }

        private void StopMovement()
        {
            lock (_lockObject)
            {
                _cancelMovementTokensource.Cancel();
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
