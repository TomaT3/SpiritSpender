﻿using SpiritSpenderServer.Persistence.SpiritDispenserSettings;
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
        {
            _spiritSpenderMotor = spiritSpenderMotor;
            _spiritDispenserSettingRepository = dispenserSettingRepository;
            _name = "SpiritDispenser1";
        }

        public async Task UpdateSettings()
        {
            _spiritDispenserSetting = await _spiritDispenserSettingRepository.GetSpiritDispenserSetting(_name);
            if (_spiritDispenserSetting == null)
            {
                _spiritDispenserSetting = new SpiritDispenserSetting
                {
                    Name = _name,
                    DriveTimeToCloseTheSpiritSpender = new Duration(1, DurationUnit.Second),
                    DriveTimeToReleaseTheSpirit = new Duration(1, DurationUnit.Second),
                    WaitTimeUntilSpiritIsReleased = new Duration(2, DurationUnit.Second),
                };

                await _spiritDispenserSettingRepository.Create(_spiritDispenserSetting);
            }
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