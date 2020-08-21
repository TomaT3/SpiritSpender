using System;
using System.Device.Gpio;
using System.Diagnostics;
using System.Threading;
using UnitsNet;

namespace SpiritSpenderServer.HardwareControl.StepperDrive
{
    public class StepperMotorControl : IStepperMotorControl
    {
        private static PinValue FORWARD = PinValue.High;
        private static PinValue BACKWARD = PinValue.Low;
        private static PinValue ENA_RELEASED = PinValue.High;
        private static PinValue ENA_LOCKED = PinValue.Low;

        private volatile bool _stopMoving;

        GpioPin _enablePin;
        GpioPin _directionPin;
        GpioPin _stepPin;
        GpioPin _referenceSwitchPin;

        public StepperMotorControl(DrivePins drivePins, IGpioControllerFacade gpioControllerFacade)
        {
            _enablePin = new GpioPin(gpioControllerFacade, drivePins.EnablePin, PinMode.Output);
            _enablePin.Write(ENA_RELEASED);

            _directionPin = new GpioPin(gpioControllerFacade, drivePins.DirectionPin, PinMode.Output);
            _directionPin.Write(BACKWARD);

            _stepPin = new GpioPin(gpioControllerFacade, drivePins.StepPin, PinMode.Output);
            _stepPin.Write(PinValue.Low);

            _referenceSwitchPin = new GpioPin(gpioControllerFacade, drivePins.ReferenceSwitchPin, PinMode.Input);
        }

        public Length CurrentPosition { get; private set; }

        public void SetPosition(Length position) => CurrentPosition = position;

        public void SetOutput(Duration[] waitTimeBetweenSteps, bool direction, Length distanceToAddForOneStep)
        {
            var ticksToWaitBetweenSteps = WaitTimeToTicks(waitTimeBetweenSteps);
            EnableDrive();
            SetDirection(direction);

            Thread.Sleep(500);

            for (int i = 0; i < ticksToWaitBetweenSteps.Length; i++)
            {
                if (_stopMoving) 
                    break;

                DoOneStep(ticksToWaitBetweenSteps[i]);
                CurrentPosition += distanceToAddForOneStep;
            }

            ReleaseDrive();
        }

        public void DriveToReferenceSwitch(Duration waitTimeBetweenSteps, bool direction)
        {
            var ticksToWaitBetweenSteps = WaitTimeToTick(waitTimeBetweenSteps);
            EnableDrive();
            SetDirection(direction);

            Thread.Sleep(500);

            while (!IsReferencePositionReached() && !_stopMoving)
            {
                DoOneStep(ticksToWaitBetweenSteps);
            }

            ReleaseDrive();
        }

        public void StopDrives()
        {
            _stopMoving = true;
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
            _stopMoving = false;
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
