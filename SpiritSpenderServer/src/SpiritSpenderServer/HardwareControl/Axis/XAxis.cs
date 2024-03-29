﻿namespace SpiritSpenderServer.HardwareControl.Axis;

using Microsoft.Extensions.Options;
using SpiritSpenderServer.Config;
using SpiritSpenderServer.HardwareControl.Axis.StepperDrive;
using SpiritSpenderServer.HardwareControl.EmergencyStop;
using SpiritSpenderServer.Interface.HardwareControl;
using SpiritSpenderServer.Persistence.DriveSettings;
using UnitsNet;
using UnitsNet.Units;

public class XAxis : AbstractAxis, IXAxis
{
    private readonly IStepperDriveControl _stepperDriveControl;
    private readonly DriveSetting _defaultDriveSetting;

    public override string Name => "X";
    internal override IStepperDriveControl StepperDriveControl => _stepperDriveControl;
    internal override DriveSetting DefaultDriveSetting => _defaultDriveSetting;

    public XAxis(IDriveSettingRepository driveSettingRepository, IEmergencyStop emergencyStop, IGpioPinFactory gpioPinFactory, IOptions<CommonServerSettings> commonServerSettings)
        : base(driveSettingRepository, emergencyStop)
    {
        var drivePins = new DrivePins
        {
            EnablePin = 4,
            DirectionPin = 17,
            StepPin = 27,
            ReferenceSwitchPin = 20
        };
        _stepperDriveControl = new StepperDriveControl(drivePins, gpioPinFactory, commonServerSettings.Value.EnableSignalR);

        _defaultDriveSetting = new DriveSetting
        {
            DriveName = Name,
            MaxSpeed = new Speed(50, SpeedUnit.MillimeterPerSecond),
            Acceleration = new Acceleration(40, AccelerationUnit.MillimeterPerSecondSquared),
            SpindlePitch = new Length(4, LengthUnit.Millimeter),
            StepsPerRevolution = 400,
            SoftwareLimitMinus = new Length(0.1, LengthUnit.Millimeter),
            SoftwareLimitPlus = new Length(248.1, LengthUnit.Millimeter),
            ReverseDirection = false,
            ReferenceDrivingDirection = DrivingDirection.Negative,
            ReferencePosition = new Length(0, LengthUnit.Millimeter),
            ReferenceDrivingSpeed = new Speed(3, SpeedUnit.MillimeterPerSecond)
        };

    }
}
