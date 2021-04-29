using System;
using System.Device.Gpio;
using System.Diagnostics;
using System.Threading;
using UnitsNet;

namespace SpiritSpenderServer.HardwareControl.Axis.StepperDrive
{
    using System.Threading.Tasks;

    public class StepperDriveControl : IStepperDriveControl
    {
        private static PinValue FORWARD = PinValue.High;
        private static PinValue BACKWARD = PinValue.Low;
        private static PinValue ENA_RELEASED = PinValue.High;
        private static PinValue ENA_LOCKED = PinValue.Low;

        GpioPin _enablePin;
        GpioPin _directionPin;
        GpioPin _stepPin;
        GpioPin _referenceSwitchPin;
        private Length _currentPosition;

        public StepperDriveControl(DrivePins drivePins, IGpioControllerFacade gpioControllerFacade)
        {
            _enablePin = new GpioPin(gpioControllerFacade, drivePins.EnablePin, PinMode.Output);
            _enablePin.Write(ENA_RELEASED);

            _directionPin = new GpioPin(gpioControllerFacade, drivePins.DirectionPin, PinMode.Output);
            _directionPin.Write(BACKWARD);

            _stepPin = new GpioPin(gpioControllerFacade, drivePins.StepPin, PinMode.Output);
            _stepPin.Write(PinValue.Low);

            _referenceSwitchPin = new GpioPin(gpioControllerFacade, drivePins.ReferenceSwitchPin, PinMode.Input);
        }

        public event Action<Length> PositionChanged;

        public Length CurrentPosition
        {
            get => _currentPosition;
            private set
            {
                _currentPosition = value;
                Task.Run(() => PositionChanged?.Invoke(CurrentPosition));
            }
        }

        public void SetPosition(Length position) => CurrentPosition = position;

        public void SetOutput(Duration[] waitTimeBetweenSteps, bool direction, Length distanceToAddForOneStep, CancellationToken token)
        {
            var ticksToWaitBetweenSteps = WaitTimeToTicks(waitTimeBetweenSteps);
            EnableDrive();
            SetDirection(direction);

            Thread.Sleep(500);

            for (int i = 0; i < ticksToWaitBetweenSteps.Length; i++)
            {
                if (token.IsCancellationRequested)
                    break;

                DoOneStep(ticksToWaitBetweenSteps[i]);
                CurrentPosition += distanceToAddForOneStep;
                
            }

            ReleaseDrive();
        }

        public void DriveToReferenceSwitch(Duration waitTimeBetweenSteps, bool direction, CancellationToken token)
        {
            var ticksToWaitBetweenSteps = WaitTimeToTick(waitTimeBetweenSteps);
            EnableDrive();
            SetDirection(direction);

            Thread.Sleep(500);

            while (!IsReferencePositionReached() && !token.IsCancellationRequested)
            {
                DoOneStep(ticksToWaitBetweenSteps);
            }

            ReleaseDrive();
        }

        private void DoOneStep(long ticksToWaitBetweenOneStep)
        {
            _stepPin.Write(PinValue.High);
            DoNothing(ticksToWaitBetweenOneStep);

            _stepPin.Write(PinValue.Low);
            DoNothing(ticksToWaitBetweenOneStep);
        }

        private void EnableDrive()
        {
            _enablePin.Write(ENA_LOCKED);
        }

        private void ReleaseDrive()
        {
            _enablePin.Write(ENA_RELEASED);
        }

        private void SetDirection(bool direction)
        {
            _directionPin.Write(direction ? FORWARD : BACKWARD);
        }

        private long[] WaitTimeToTicks(Duration[] waitTimeBetweenSteps)
        {
            long[] ticksToWaitBetweenSteps = new long[waitTimeBetweenSteps.Length];
            for (int i = 0; i < waitTimeBetweenSteps.Length; i++)
            {
                ticksToWaitBetweenSteps[i] = WaitTimeToTick(waitTimeBetweenSteps[i]);
            }

            return ticksToWaitBetweenSteps;
        }

        private bool IsReferencePositionReached()
        {
            var referenceSwitchValue = _referenceSwitchPin.Read();
            return referenceSwitchValue == PinValue.High;
        }
        private static long WaitTimeToTick(Duration waitTime)
        {
            return Convert.ToInt64(Math.Round(waitTime.Seconds * Stopwatch.Frequency));
        }

        private static void DoNothing(long ticksToWait)
        {
            var sw = Stopwatch.StartNew();

            while (sw.ElapsedTicks < ticksToWait)
            {
            }
        }
    }
}
