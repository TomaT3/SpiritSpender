using SpiritSpenderServer.HardwareControl.Axis.StepperDrive;
using SpiritSpenderServer.HardwareControl.EmergencyStop;
using SpiritSpenderServer.Persistence.DriveSettings;
using UnitsNet;
using UnitsNet.Units;

namespace SpiritSpenderServer.HardwareControl.Axis
{
    public class XAxis : AbstractAxis, IXAxis
    {
        private IStepperDriveControl _stepperDriveControl;
        private DriveSetting _defaultDriveSetting;

        public override string Name => "X";
        internal override IStepperDriveControl StepperDriveControl => _stepperDriveControl;
        internal override DriveSetting DefaultDriveSetting => _defaultDriveSetting;

        public XAxis(IDriveSettingRepository driveSettingRepository, IEmergencyStop emergencyStop, IGpioControllerFacade gpioControllerFacade)
            : base(driveSettingRepository, emergencyStop)
        {
            var drivePins = new DrivePins
            {
                EnablePin = 4,
                DirectionPin = 17,
                StepPin = 27,
                ReferenceSwitchPin = 20
            };
            _stepperDriveControl = new StepperDriveControl(drivePins, gpioControllerFacade);

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
}
