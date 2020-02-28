using SpiritSpenderServer.Persistence.SpiritDispenserSettings;
using System;
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

        public SpiritDispenserControl(ISpiritSpenderMotor spiritSpenderMotor, ISpiritDispenserSettingRepository dispenserSettingRepository, string name)
            => (_spiritSpenderMotor, _spiritDispenserSettingRepository, _name) = (spiritSpenderMotor, dispenserSettingRepository, name);

        public string Name => _name;

        public async Task UpdateSettingsAsync()
        {
            _spiritDispenserSetting = await _spiritDispenserSettingRepository.GetSpiritDispenserSetting(_name);
        }

        public void FillGlas()
        {
            ReleaseSpirit();
            Thread.Sleep(Convert.ToInt32(_spiritDispenserSetting.WaitTimeUntilSpiritIsReleased.Milliseconds));
            CloseSpiritSpender();
        }

        public void ReleaseSpirit()
        {
            _spiritSpenderMotor.DriveForward(_spiritDispenserSetting.DriveTimeToReleaseTheSpirit);
        }

        public void CloseSpiritSpender()
        {
            _spiritSpenderMotor.DriveForward(_spiritDispenserSetting.DriveTimeToReleaseTheSpirit);
        }
    }
}
