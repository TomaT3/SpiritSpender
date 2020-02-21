using SpiritSpenderServer.Persistence.DriveSetings;
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
        private static PinValue FORWARD = PinValue.High;
        private static PinValue BACKWARD = PinValue.Low;
        private static PinValue ENA_RELEASED = PinValue.High;
        private static PinValue ENA_LOCKED = PinValue.Low;

        GpioPin _enablePin;
        GpioPin _directionPin;
        GpioPin _stepPin;

        private string _driveName;
        private IDriveSettingRepository _driveSettingRepository;
        private DriveSetting _driveSetting;
        private Length _lengthOfOneStep;

        public StepperDrive(string driveName, IDriveSettingRepository driveSettingRepository)
        {
            _driveName = driveName;
            _driveSettingRepository = driveSettingRepository;
        }

        public Length CurrentPosition { get; private set; }

        public async Task InitDriveAsync()
        {
            _driveSetting = await _driveSettingRepository.GetDriveSetting(_driveName);
            if (_driveSetting == null)
            {
                _driveSetting = new DriveSetting
                {
                    DriveName = _driveName,
                    Acceleration = new Acceleration(20, AccelerationUnit.MillimeterPerSecondSquared),
                    MaxSpeed = new Speed(200, SpeedUnit.MillimeterPerSecond),
                    SpindelPitch = new Length(8, LengthUnit.Millimeter),
                    StepsPerRevolution = 400,
                    EnableGpioPin = 17,
                    DirectionGpioPin = 27,
                    StepGpioPin = 22,
                    ReverseDirection = false
                };
                await _driveSettingRepository.Create(_driveSetting);
            }

            _lengthOfOneStep = 1.ToDistance(_driveSetting);
            SetGpio();
        }

        private void SetGpio()
        {
            GpioController controller = new GpioController();
            _enablePin = new GpioPin(controller, _driveSetting.EnableGpioPin, PinMode.Output);
            _enablePin.Write(ENA_RELEASED);

            _stepPin = new GpioPin(controller, _driveSetting.StepGpioPin, PinMode.Output);
            _stepPin.Write(PinValue.Low);


            if (_driveSetting.ReverseDirection)
            {
                FORWARD = PinValue.Low;
                BACKWARD = PinValue.High;
            }
            else
            {
                FORWARD = PinValue.High;
                BACKWARD = PinValue.Low;
            }

            _directionPin = new GpioPin(controller, _driveSetting.DirectionGpioPin, PinMode.Output);
            _directionPin.Write(FORWARD);
        }

        public void DriveToPosition(Length position)
        {
            var distanceToGo = CurrentPosition - position;
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

            PinValue direction;
            Length distanceToAdd;
            if (steps > 0)
            {
                direction = FORWARD;
                distanceToAdd = _lengthOfOneStep;
            }
            else
            {
                direction = BACKWARD;
                distanceToAdd = _lengthOfOneStep * -1;
            }

            SetOutput(waitTimeBetweenSteps, direction, distanceToAdd);
        }

        private void SetOutput(double[] waitTimeBetweenSteps, PinValue direction, Length distanceToAdd)
        {
            _enablePin.Write(ENA_LOCKED);
            _directionPin.Write(direction);
            DoNothing(500);

            for (int i = 0; i < waitTimeBetweenSteps.Length; i++)
            {
                _stepPin.Write(PinValue.High);
                DoNothing(waitTimeBetweenSteps[i]);

                _stepPin.Write(PinValue.Low);
                DoNothing(waitTimeBetweenSteps[i]);

                CurrentPosition += distanceToAdd;
            }

            _enablePin.Write(ENA_RELEASED);
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

        private static void DoNothing(double durationMilliseconds)
        {
            var sw = Stopwatch.StartNew();

            while (sw.Elapsed.Milliseconds < durationMilliseconds)
            {

            }
        }
    }
}
