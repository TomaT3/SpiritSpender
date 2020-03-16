﻿using SpiritSpenderServer.HardwareControl;
using SpiritSpenderServer.HardwareControl.SpiritSpenderMotor;
using SpiritSpenderServer.HardwareControl.StepperDrive;
using SpiritSpenderServer.Persistence.DriveSettings;
using SpiritSpenderServer.Persistence.SpiritDispenserSettings;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitsNet;
using UnitsNet.Units;

namespace SpiritSpenderServer.Config.HardwareConfiguration
{
    public class HardwareConfiguration : IHardwareConfiguration
    {
        private IGpioControllerFacade _gpioControllerFacade;
        private ISpiritDispenserSettingRepository _spiritDispenserSettingRepository;
        private IDriveSettingRepository _driveSettingRepository;

        public HardwareConfiguration(ISpiritDispenserSettingRepository spiritDispenserSettingRepository, IDriveSettingRepository driveSettingRepository, IGpioControllerFacade gpioControllerFacade)
            => (_spiritDispenserSettingRepository, _driveSettingRepository, _gpioControllerFacade) = (spiritDispenserSettingRepository, driveSettingRepository, gpioControllerFacade);

        public ISpiritDispenserControl SpiritDispenserControl { get; private set; }

        public Dictionary<string, IStepperDrive> StepperDrives { get; private set; }

        public async Task LoadHardwareConfiguration()
        {
            await AddSpiritDispenserControl();
            await AddDrives();
        }

        private async Task AddSpiritDispenserControl()
        {
            const string SPIRIT_DISPENSER_NAME = "SpiritDispenser";
            var settings = await _spiritDispenserSettingRepository.GetSpiritDispenserSetting(SPIRIT_DISPENSER_NAME);
            if (settings == null)
            {
                settings = new SpiritDispenserSetting
                {
                    Name = SPIRIT_DISPENSER_NAME,
                    DriveTimeToCloseTheSpiritSpender = new Duration(1, DurationUnit.Second),
                    DriveTimeToReleaseTheSpirit = new Duration(1, DurationUnit.Second),
                    WaitTimeUntilSpiritIsReleased = new Duration(2, DurationUnit.Second),
                };

                await _spiritDispenserSettingRepository.Create(settings);
            }

            SpiritDispenserControl = new SpiritDispenserControl(
                new SpiritSpenderMotor(forwardGpioPin: 18, backwardGpioPin: 23, gpioControllerFacade: _gpioControllerFacade),
                _spiritDispenserSettingRepository, SPIRIT_DISPENSER_NAME);
            await SpiritDispenserControl.UpdateSettingsAsync();
        }

        private async Task AddDrives()
        {
            StepperDrives = new Dictionary<string, IStepperDrive>();
            await AddStepperDriveX();
            await AddStepperDriveY();
        }

        private async Task AddStepperDriveX()
        {
            const string DRIVE_NAME = "X";
            var driveSetting = await _driveSettingRepository.GetDriveSetting(DRIVE_NAME);
            if (driveSetting == null)
            {
                driveSetting = new DriveSetting
                {
                    DriveName = DRIVE_NAME,
                    Acceleration = new Acceleration(20, AccelerationUnit.MillimeterPerSecondSquared),
                    MaxSpeed = new Speed(200, SpeedUnit.MillimeterPerSecond),
                    SpindelPitch = new Length(8, LengthUnit.Millimeter),
                    StepsPerRevolution = 400,
                    ReverseDirection = false
                };

                await _driveSettingRepository.Create(driveSetting);
            }

            var stepperDrive = new StepperDrive(DRIVE_NAME, _driveSettingRepository,
                new StepperMotorControl(enablePin: 4, directionPin: 17, stepPin: 27, gpioControllerFacade: _gpioControllerFacade));
            await stepperDrive.UpdateSettingsAsync();
            StepperDrives.Add(DRIVE_NAME, stepperDrive);
        }

        private async Task AddStepperDriveY()
        {
            const string DRIVE_NAME = "Y";
            var driveSetting = await _driveSettingRepository.GetDriveSetting(DRIVE_NAME);
            if (driveSetting == null)
            {
                driveSetting = new DriveSetting
                {
                    DriveName = DRIVE_NAME,
                    Acceleration = new Acceleration(20, AccelerationUnit.MillimeterPerSecondSquared),
                    MaxSpeed = new Speed(200, SpeedUnit.MillimeterPerSecond),
                    SpindelPitch = new Length(8, LengthUnit.Millimeter),
                    StepsPerRevolution = 400,
                    ReverseDirection = false
                };

                await _driveSettingRepository.Create(driveSetting);
            }

            var stepperDrive = new StepperDrive(DRIVE_NAME, _driveSettingRepository,
                new StepperMotorControl(enablePin: 22, directionPin: 5, stepPin: 6, gpioControllerFacade: _gpioControllerFacade));
            await stepperDrive.UpdateSettingsAsync();
            StepperDrives.Add(DRIVE_NAME, stepperDrive);
        }
    }
}