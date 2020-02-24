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

        public Length CurrentPosition => _stepperMotorControl.CurrentPosition;

        public async Task UpdateSettingsAsync()
        {
            _driveSetting = await _driveSettingRepository.GetDriveSetting(_driveName);
            _lengthOfOneStep = 1.ToDistance(_driveSetting);
        }


        public void DriveToPosition(Length position)
        {
            var distanceToGo = _stepperMotorControl.CurrentPosition - position;
            DriveSteps(distanceToGo.ToSteps(_driveSetting));
        }

        public void DriveDistance(Length distance)
        {
            var distanceToGo = distance;
            DriveSteps(distanceToGo.ToSteps(_driveSetting));
        }

        private void DriveSteps(int steps)
        {
            var maxSpeedinStepsPerSecond = _driveSetting.MaxSpeed.ToStepsPerSecond(_driveSetting);
            var maxAccelerationInStepsPerSecondSquare = _driveSetting.Acceleration.ToAccelerationPerSecondSquare(_driveSetting);
            var numberOfStepsForAcceleration = Convert.ToInt32(Math.Pow(maxSpeedinStepsPerSecond, 2) / maxAccelerationInStepsPerSecondSquare / 2);
            var numberOfStepsForDecceleration = numberOfStepsForAcceleration;
            var numberOfStepsWithMaxSpeed = 0;

            if ((2 * numberOfStepsForAcceleration) < steps)
            {
                numberOfStepsForDecceleration = numberOfStepsForAcceleration;
                numberOfStepsWithMaxSpeed = steps - numberOfStepsForAcceleration - numberOfStepsForDecceleration;
            }
            else
            {
                numberOfStepsForAcceleration = steps / 2;
                numberOfStepsForDecceleration = steps / 2;
            }

            var waitTimeBetweenSteps = CalculateWaitTimeBetweenSteps(
                numberOfStepsForAcceleration,
                numberOfStepsWithMaxSpeed,
                numberOfStepsForDecceleration,
                maxSpeedinStepsPerSecond,
                maxAccelerationInStepsPerSecondSquare);

            bool direction;
            Length distanceToAdd;
            if (steps > 0 && !_driveSetting.ReverseDirection ||
                steps < 0 && _driveSetting.ReverseDirection)
            {
                direction = FORWARD;
                distanceToAdd = _lengthOfOneStep;
            }
            else
            {
                direction = BACKWARD;
                distanceToAdd = _lengthOfOneStep * -1;
            }

            _stepperMotorControl.SetOutput(waitTimeBetweenSteps, direction, distanceToAdd);
        }

       
        private double[] CalculateWaitTimeBetweenSteps(
            int numberOfStepsForAcceleration,
            int numberOfStepsWithMaxSpeed,
            int numberOfStepsForDecceleration,
            double maxSpeed,
            double acceleration)
        {
            double[] waitTimeBetweenAccelerationSteps = new double[numberOfStepsForAcceleration];
            double[] waitTimeBetweenMaxSpeedSteps = new double[numberOfStepsWithMaxSpeed];
            double[] waitTimeBetweenDeccelerationSteps = new double[numberOfStepsForDecceleration];

            double[] tempAccelerationArray = GetAccelerationTimeArray(numberOfStepsForAcceleration, acceleration);

            for (int i = 0; i < tempAccelerationArray.Length; i++)
            {
                if (i != tempAccelerationArray.Length - 1)
                    waitTimeBetweenAccelerationSteps[i] = (tempAccelerationArray[i + 1] - tempAccelerationArray[i]) / 2.0;
                else
                    waitTimeBetweenAccelerationSteps[i] = 1.0 / maxSpeed / 2.0;
            }

            for (int i = 0; i < numberOfStepsWithMaxSpeed; i++)
            {
                waitTimeBetweenMaxSpeedSteps[i] = 1 / maxSpeed / 2.0;
            }

            int j = 0;
            for (int i = waitTimeBetweenAccelerationSteps.Length - 1; i >= 0; i--)
            {
                waitTimeBetweenDeccelerationSteps[j] = waitTimeBetweenAccelerationSteps[i];
                j++;
            }

            ConvertArrayFromSecondsToMilliseconds(waitTimeBetweenAccelerationSteps);
            ConvertArrayFromSecondsToMilliseconds(waitTimeBetweenMaxSpeedSteps);
            ConvertArrayFromSecondsToMilliseconds(waitTimeBetweenDeccelerationSteps);

            List<double> waitTimeBetweenSteps = new List<double>();

            waitTimeBetweenSteps = waitTimeBetweenAccelerationSteps.ToList<double>()
                .Concat<double>(waitTimeBetweenMaxSpeedSteps.ToList<double>())
                .Concat<double>(waitTimeBetweenDeccelerationSteps.ToList<double>())
                .ToList();

            return waitTimeBetweenSteps.ToArray();
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

        private void ConvertArrayFromSecondsToMilliseconds(double[] arrayToConvert)
        {
            for (int i = 0; i < arrayToConvert.Length; i++)
            {
                arrayToConvert[i] = arrayToConvert[i] * 1000.0;
            }
        }
    }
}
