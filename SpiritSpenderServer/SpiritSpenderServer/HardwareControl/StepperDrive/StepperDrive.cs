using SpiritSpenderServer.Persistence.DriveSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitsNet;
using System.Threading.Tasks;
using UnitsNet.Units;
using System.Device.Gpio;
using System.Diagnostics;

namespace SpiritSpenderServer.HardwareControl.StepperDrive
{
    public class StepperDrive : IStepperDrive
    {
        private static bool FORWARD = true;
        private static bool BACKWARD = false;

        private string _driveName;
        private IStepperMotorControl _stepperMotorControl;
        private IDriveSettingRepository _driveSettingRepository;
        private DriveSetting _driveSetting;
        private Length _lengthOfOneStep;

       

        public StepperDrive(string driveName, IDriveSettingRepository driveSettingRepository, IStepperMotorControl stepperMotorControl)
            => (_driveName, _driveSettingRepository, _stepperMotorControl) = (driveName, driveSettingRepository, stepperMotorControl);

        public Length CurrentPosition => _stepperMotorControl.CurrentPosition.ToUnit(LengthUnit.Millimeter);

        public void SetPosition(Length position) => _stepperMotorControl.SetPosition(position);

        public async Task UpdateSettingsAsync()
        {
            _driveSetting = await _driveSettingRepository.GetDriveSetting(_driveName);
            _lengthOfOneStep = 1.ToDistance(_driveSetting);
        }

        public void ReferenceDrive()
        {
            var referenceSpeedInStepsPerSecond = _driveSetting.ReferenceDrivingSpeed.ToStepsPerSecond(_driveSetting);
            var waitTimeBetweenSteps = new Duration(1 / referenceSpeedInStepsPerSecond / 2.0, DurationUnit.Second);
            var direction = GetReferenceDirection();

            _stepperMotorControl.ReferenceDrive(waitTimeBetweenSteps, direction, _driveSetting.ReferencePosition);
        }

        public void DriveToPosition(Length position)
        {
            var distanceToGo = _stepperMotorControl.CurrentPosition - position;
            DriveSteps(distanceToGo);
        }

        public void DriveDistance(Length distance)
        {
            DriveSteps(distance);
        }

        private bool CheckLimitSwitches(Length distance)
        {
            var endPosition = _stepperMotorControl.CurrentPosition + distance;
            if ( endPosition < _driveSetting.SoftwareLimitMinus
                || endPosition > _driveSetting.SoftwareLimitPlus)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool DriveSteps(Length distanceToGo)
        {
            var isDistanceOk = CheckLimitSwitches(distanceToGo);
            if (!isDistanceOk)
                return false;

            var steps = distanceToGo.ToSteps(_driveSetting);
            var numberOfSteps = Math.Abs(steps);
            var maxSpeedinStepsPerSecond = _driveSetting.MaxSpeed.ToStepsPerSecond(_driveSetting);
            var maxAccelerationInStepsPerSecondSquare = _driveSetting.Acceleration.ToAccelerationPerSecondSquare(_driveSetting);
            var numberOfStepsForAcceleration = Convert.ToInt32(Math.Pow(maxSpeedinStepsPerSecond, 2) / maxAccelerationInStepsPerSecondSquare / 2);
            var numberOfStepsForDecceleration = numberOfStepsForAcceleration;
            var numberOfStepsWithMaxSpeed = 0;

            if ((2 * numberOfStepsForAcceleration) < numberOfSteps)
            {
                numberOfStepsForDecceleration = numberOfStepsForAcceleration;
                numberOfStepsWithMaxSpeed = numberOfSteps - numberOfStepsForAcceleration - numberOfStepsForDecceleration;
            }
            else
            {
                numberOfStepsForAcceleration = numberOfSteps / 2;
                numberOfStepsForDecceleration = numberOfSteps / 2;
            }

            var waitTimeBetweenSteps = CalculateWaitTimeBetweenSteps(
                numberOfStepsForAcceleration,
                numberOfStepsWithMaxSpeed,
                numberOfStepsForDecceleration,
                maxSpeedinStepsPerSecond,
                maxAccelerationInStepsPerSecondSquare);

            var drive = GetDriveDirection(steps);

            _stepperMotorControl.SetOutput(waitTimeBetweenSteps, drive.direction, drive.distanceOfOneStep);
            return true;
        }

        private (bool direction, Length distanceOfOneStep) GetDriveDirection(int steps)
        {
            if (steps > 0 && !_driveSetting.ReverseDirection ||
                steps < 0 && _driveSetting.ReverseDirection)
            {
                return (FORWARD, _lengthOfOneStep);
            }
            else
            {
                return (BACKWARD, _lengthOfOneStep * -1);
            }
        }

        private bool GetReferenceDirection()
        {
            var direction = _driveSetting.ReferenceDrivingDirection == DrivingDirection.Positive ? FORWARD : BACKWARD;

            if (_driveSetting.ReverseDirection)
            {
                direction = !direction;
            }

            return direction;
        }
       
        private Duration[] CalculateWaitTimeBetweenSteps(
            int numberOfStepsForAcceleration,
            int numberOfStepsWithMaxSpeed,
            int numberOfStepsForDecceleration,
            double maxSpeed,
            double acceleration)
        {
            Duration[] waitTimeBetweenAccelerationSteps = new Duration[numberOfStepsForAcceleration];
            Duration[] waitTimeBetweenMaxSpeedSteps = new Duration[numberOfStepsWithMaxSpeed];
            Duration[] waitTimeBetweenDeccelerationSteps = new Duration[numberOfStepsForDecceleration];

            double[] tempAccelerationArray = GetAccelerationTimeArray(numberOfStepsForAcceleration, acceleration);

            for (int i = 0; i < tempAccelerationArray.Length; i++)
            {
                if (i != tempAccelerationArray.Length - 1)
                    waitTimeBetweenAccelerationSteps[i] = new Duration((tempAccelerationArray[i + 1] - tempAccelerationArray[i]) / 2.0, DurationUnit.Second);
                else
                    waitTimeBetweenAccelerationSteps[i] = new Duration(1.0 / maxSpeed / 2.0, DurationUnit.Second);
            }

            for (int i = 0; i < numberOfStepsWithMaxSpeed; i++)
            {
                waitTimeBetweenMaxSpeedSteps[i] = new Duration(1.0 / maxSpeed / 2.0, DurationUnit.Second);
            }

            int j = 0;
            for (int i = waitTimeBetweenAccelerationSteps.Length - 1; i >= 0; i--)
            {
                waitTimeBetweenDeccelerationSteps[j] = waitTimeBetweenAccelerationSteps[i];
                j++;
            }

            var waitTimeBetweenSteps = waitTimeBetweenAccelerationSteps.ToList<Duration>()
                .Concat<Duration>(waitTimeBetweenMaxSpeedSteps.ToList<Duration>())
                .Concat<Duration>(waitTimeBetweenDeccelerationSteps.ToList<Duration>())
                .ToArray();

            return waitTimeBetweenSteps;
        }

        private static double[] GetAccelerationTimeArray(int numberOfStepsForAcceleration, double acceleration)
        {
            double[] tempAccelerationArray = new double[numberOfStepsForAcceleration];
            for (int i = 0; i < numberOfStepsForAcceleration; i++)
            {
                tempAccelerationArray[i] = Math.Sqrt(2.0 * i / acceleration);
            }

            return tempAccelerationArray;
        }
    }
}
