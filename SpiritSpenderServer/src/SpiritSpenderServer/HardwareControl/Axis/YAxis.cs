using Microsoft.Extensions.Options;
using SpiritSpenderServer.Config;
using SpiritSpenderServer.HardwareControl.Axis.StepperDrive;
using SpiritSpenderServer.HardwareControl.EmergencyStop;
using SpiritSpenderServer.Interface.HardwareControl;
using SpiritSpenderServer.Persistence.DriveSettings;
using UnitsNet;
using UnitsNet.Units;

namespace SpiritSpenderServer.HardwareControl.Axis
{
    public class YAxis : AbstractAxis, IYAxis
    {
        private IStepperDriveControl _stepperDriveControl;
        private DriveSetting _defaultDriveSetting;

        public override string Name => "Y";
        internal override IStepperDriveControl StepperDriveControl => _stepperDriveControl;
        internal override DriveSetting DefaultDriveSetting => _defaultDriveSetting;


        public YAxis(IDriveSettingRepository driveSettingRepository, IEmergencyStop emergencyStop, IGpioPinFactory gpioPinFactory, IOptions<CommonServerSettings> commonServerSettings)
            : base(driveSettingRepository, emergencyStop)
        {
            var drivePins = new DrivePins
            {
                EnablePin = 22,
                DirectionPin = 5,
                StepPin = 6,
                ReferenceSwitchPin = 21
            };
            _stepperDriveControl = new StepperDriveControl(drivePins, gpioPinFactory, commonServerSettings.Value.EnableSignalR);

            _defaultDriveSetting = new DriveSetting
            {
                DriveName = Name,
                MaxSpeed = new Speed(80, SpeedUnit.MillimeterPerSecond),
                Acceleration = new Acceleration(80, AccelerationUnit.MillimeterPerSecondSquared),
                SpindlePitch = new Length(8, LengthUnit.Millimeter),
                StepsPerRevolution = 400,
                SoftwareLimitMinus = new Length(0.1, LengthUnit.Millimeter),
                SoftwareLimitPlus = new Length(141.1, LengthUnit.Millimeter),
                ReverseDirection = false,
                ReferenceDrivingDirection = DrivingDirection.Negative,
                ReferencePosition = new Length(0, LengthUnit.Millimeter),
                ReferenceDrivingSpeed = new Speed(3.5, SpeedUnit.MillimeterPerSecond)
            };
        }
    }
}
