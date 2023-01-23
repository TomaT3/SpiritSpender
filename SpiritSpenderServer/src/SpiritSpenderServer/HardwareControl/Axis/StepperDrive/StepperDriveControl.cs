using System;
using System.Device.Gpio;
using System.Diagnostics;
using System.Threading;
using UnitsNet;

namespace SpiritSpenderServer.HardwareControl.Axis.StepperDrive
{
    using SpiritSpenderServer.Interface.HardwareControl;
    using System.Threading.Tasks;

    public class StepperDriveControl : IStepperDriveControl
    {
        private static PinValue FORWARD = PinValue.High;
        private static PinValue BACKWARD = PinValue.Low;
        private static PinValue ENA_RELEASED = PinValue.High;
        private static PinValue ENA_LOCKED = PinValue.Low;

        private bool _enableSignalR;

        IGpioPin _enablePin;
        IGpioPin _directionPin;
        IGpioPin _stepPin;
        IGpioPin _referenceSwitchPin;
        private Length _currentPosition;

        public StepperDriveControl(DrivePins drivePins, IGpioPinFactory gpioPinFactory, bool enableSignalR)
        {
            _enablePin = gpioPinFactory.CreateGpioPin(drivePins.EnablePin, PinMode.Output);
            _enablePin.Write(ENA_RELEASED);

            _directionPin = gpioPinFactory.CreateGpioPin(drivePins.DirectionPin, PinMode.Output);
            _directionPin.Write(BACKWARD);

            _stepPin = gpioPinFactory.CreateGpioPin(drivePins.StepPin, PinMode.Output);
            _stepPin.Write(PinValue.Low);

            _referenceSwitchPin = gpioPinFactory.CreateGpioPin(drivePins.ReferenceSwitchPin, PinMode.Input);

            _enableSignalR = enableSignalR;
        }

        public event Action<Length>? PositionChanged;

        public Length CurrentPosition
        {
            get => _currentPosition;
            private set
            {
                _currentPosition = value;
                if (_enableSignalR)
                {
                    Task.Run(() => PositionChanged?.Invoke(CurrentPosition));
                }
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
