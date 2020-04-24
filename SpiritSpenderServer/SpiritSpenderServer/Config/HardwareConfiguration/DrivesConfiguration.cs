using SpiritSpenderServer.HardwareControl;
using SpiritSpenderServer.HardwareControl.StepperDrive;
using SpiritSpenderServer.Persistence.DriveSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitsNet;
using UnitsNet.Units;

namespace SpiritSpenderServer.Config.HardwareConfiguration
{
    public class DrivesConfiguration
    {
        public static async Task<StepperDrive> GetStepperDriveX(IDriveSettingRepository driveSettingRepository, IGpioControllerFacade gpioControllerFacade)
        {
            const string DRIVE_NAME = "X";
            var driveSetting = await driveSettingRepository.GetDriveSetting(DRIVE_NAME);
            if (driveSetting == null)
            {
                driveSetting = new DriveSetting
                {
                    DriveName = DRIVE_NAME,
                    Acceleration = new Acceleration(20, AccelerationUnit.MillimeterPerSecondSquared),
                    MaxSpeed = new Speed(200, SpeedUnit.MillimeterPerSecond),
                    SpindlePitch = new Length(4, LengthUnit.Millimeter),
                    StepsPerRevolution = 400,
                    SoftwareLimitMinus = new Length(-0.1, LengthUnit.Millimeter),
                    SoftwareLimitPlus = new Length(248.1, LengthUnit.Millimeter),
                    ReverseDirection = false,
                    ReferenceDrivingDirection = DrivingDirection.Negative,
                    ReferencePosition = new Length(0, LengthUnit.Millimeter),
                    ReferenceDrivingSpeed = new Speed(4, SpeedUnit.MillimeterPerSecond)
                };

                await driveSettingRepository.Create(driveSetting);
            }

            var drivePins = new DrivePins
            {
                EnablePin = 4,
                DirectionPin = 17,
                StepPin = 27,
                ReferenceSwitchPin = 20
            };

            var stepperDrive = new StepperDrive(DRIVE_NAME, driveSettingRepository,
                new StepperMotorControl(drivePins, gpioControllerFacade));
            await stepperDrive.UpdateSettingsAsync();
            return stepperDrive;
        }

        public static async Task<StepperDrive> GetStepperDriveY(IDriveSettingRepository driveSettingRepository, IGpioControllerFacade gpioControllerFacade)
        {
            const string DRIVE_NAME = "Y";
            var driveSetting = await driveSettingRepository.GetDriveSetting(DRIVE_NAME);
            if (driveSetting == null)
            {
                driveSetting = new DriveSetting
                {
                    DriveName = DRIVE_NAME,
                    Acceleration = new Acceleration(20, AccelerationUnit.MillimeterPerSecondSquared),
                    MaxSpeed = new Speed(200, SpeedUnit.MillimeterPerSecond),
                    SpindlePitch = new Length(8, LengthUnit.Millimeter),
                    StepsPerRevolution = 400,
                    SoftwareLimitMinus = new Length(-0.1, LengthUnit.Millimeter),
                    SoftwareLimitPlus = new Length(141.1, LengthUnit.Millimeter),
                    ReverseDirection = false,
                    ReferenceDrivingDirection = DrivingDirection.Negative,
                    ReferencePosition = new Length(0, LengthUnit.Millimeter),
                    ReferenceDrivingSpeed = new Speed(3, SpeedUnit.MillimeterPerSecond)
                };

                await driveSettingRepository.Create(driveSetting);
            }

            var drivePins = new DrivePins
            {
                EnablePin = 22,
                DirectionPin = 5,
                StepPin = 6,
                ReferenceSwitchPin = 21
            };

            var stepperDrive = new StepperDrive(DRIVE_NAME, driveSettingRepository,
                new StepperMotorControl(drivePins, gpioControllerFacade));
            await stepperDrive.UpdateSettingsAsync();
            return stepperDrive;
        }
    }
}
