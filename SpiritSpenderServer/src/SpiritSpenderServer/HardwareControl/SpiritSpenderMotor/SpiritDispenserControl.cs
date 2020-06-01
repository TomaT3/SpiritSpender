using SpiritSpenderServer.Persistence.SpiritDispenserSettings;
using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnitsNet;
using UnitsNet.Units;

namespace SpiritSpenderServer.HardwareControl.SpiritSpenderMotor
{
    public class SpiritDispenserControl : ISpiritDispenserControl
    {
        private string _name;
        private ISpiritDispenserSettingRepository _spiritDispenserSettingRepository;
        private SpiritDispenserSetting _spiritDispenserSetting;
        private ISpiritSpenderMotor _spiritSpenderMotor;
        private AutoResetEvent _waitHandleSpiritDispenserRefilled = new AutoResetEvent(true);
        private System.Timers.Timer _spiritDispenserRefilledTimer;

        public SpiritDispenserControl(ISpiritSpenderMotor spiritSpenderMotor, ISpiritDispenserSettingRepository dispenserSettingRepository, string name)
            => (_spiritSpenderMotor, _spiritDispenserSettingRepository, _name) = (spiritSpenderMotor, dispenserSettingRepository, name);

        public string Name => _name;

        public async Task UpdateSettingsAsync()
        {
            _spiritDispenserSetting = await _spiritDispenserSettingRepository.GetSpiritDispenserSetting(_name);
        }

        public void FillGlas()
        {
            _waitHandleSpiritDispenserRefilled.WaitOne();

            ReleaseSpirit();
            Thread.Sleep(Convert.ToInt32(_spiritDispenserSetting.WaitTimeUntilSpiritIsReleased.Milliseconds));
            CloseSpiritSpender();

            StartRefillTimer(_spiritDispenserSetting.WaitTimeUntilSpiritIsRefilled);
        }

        public void ReleaseSpirit()
        {
            _spiritSpenderMotor.DriveBackward(_spiritDispenserSetting.DriveTimeToReleaseTheSpirit);
        }

        public void CloseSpiritSpender()
        {
            _spiritSpenderMotor.DriveForward(_spiritDispenserSetting.DriveTimeToCloseTheSpiritSpender);
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
