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
        private AutoResetEvent _waitHandleSpiritDispenserRefilled = new AutoResetEvent(true);
        private System.Timers.Timer _spiritDispenserRefilledTimer;

        public SpiritDispenserControl(ILinearMotor spiritSpenderMotor, ISpiritDispenserSettingRepository dispenserSettingRepository, string name)
            => (_spiritSpenderMotor, _spiritDispenserSettingRepository, _name) = (spiritSpenderMotor, dispenserSettingRepository, name);

        public string Name => _name;

        public async Task UpdateSettingsAsync()
        {
            _spiritDispenserSetting = await _spiritDispenserSettingRepository.GetSpiritDispenserSetting(_name);
        }

        public async Task FillGlas()
        {
            _waitHandleSpiritDispenserRefilled.WaitOne();

            await ReleaseSpirit();
            await Task.Delay(Convert.ToInt32(_spiritDispenserSetting.WaitTimeUntilSpiritIsReleased.Milliseconds));
            await CloseSpiritSpender();

            StartRefillTimer(_spiritDispenserSetting.WaitTimeUntilSpiritIsRefilled);
        }

        public async Task ReleaseSpirit()
        {
            await _spiritSpenderMotor.DriveBackward(_spiritDispenserSetting.DriveTimeToReleaseTheSpirit);
        }

        public async Task CloseSpiritSpender()
        {
            await _spiritSpenderMotor.DriveForward(_spiritDispenserSetting.DriveTimeToCloseTheSpiritSpender);
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
