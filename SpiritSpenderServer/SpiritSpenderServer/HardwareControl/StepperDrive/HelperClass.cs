using SpiritSpenderServer.Persistence.DriveSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitsNet;
using UnitsNet.Units;

namespace SpiritSpenderServer.HardwareControl.StepperDrive
{
    public static class HelperClass
    {
        

        public static int ToSteps(this Length distance, DriveSetting driveSetting)
        {
            var steps = distance.Millimeters * driveSetting.StepsPerRevolution / driveSetting.SpindelPitch.Millimeters;
            return Convert.ToInt32(steps);
        }

        public static double ToStepsPerSecond(this Speed speed, DriveSetting driveSetting)
        {
            return speed.MillimetersPerSecond * driveSetting.StepsPerRevolution / driveSetting.SpindelPitch.Millimeters;
        }

        public static double ToAccelerationPerSecondSquare(this Acceleration acceleration, DriveSetting driveSetting)
        {
            return acceleration.MillimetersPerSecondSquared * driveSetting.StepsPerRevolution / driveSetting.SpindelPitch.Millimeters;
        }
        public static Length ToDistance(this int steps, DriveSetting driveSetting)
        {
            var distanceInMillimeters = steps * driveSetting.SpindelPitch.Millimeters / driveSetting.StepsPerRevolution;
            return new Length(distanceInMillimeters, LengthUnit.Millimeter);
        }
    }
}
