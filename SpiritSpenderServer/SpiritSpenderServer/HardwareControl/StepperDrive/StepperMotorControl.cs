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

        GpioPin _enablePin;
        GpioPin _directionPin;
        GpioPin _stepPin;

        public StepperMotorControl(int enablePin, int directionPin, int stepPin, IGpioControllerFacade gpioControllerFacade)
        {
            _enablePin = new GpioPin(gpioControllerFacade, enablePin, PinMode.Output);
            _enablePin.Write(ENA_RELEASED);

            _directionPin = new GpioPin(gpioControllerFacade, directionPin, PinMode.Output);
            _directionPin.Write(BACKWARD);

            _stepPin = new GpioPin(gpioControllerFacade, stepPin, PinMode.Output);
            _stepPin.Write(PinValue.Low);
        }

        public Length CurrentPosition { get; private set; }

        public void SetPosition(Length position) => CurrentPosition = position;

        public void SetOutput(double[] waitTimeBetweenSteps, bool direction, Length distanceToAddForOneStep)
        {
            var ticksToWaitBetweenSteps = WaitTimeToTicks(waitTimeBetweenSteps);
            _enablePin.Write(ENA_LOCKED);
            _directionPin.Write(direction ? FORWARD : BACKWARD);

            Thread.Sleep(500);

            for (int i = 0; i < ticksToWaitBetweenSteps.Length; i++)
            {
                _stepPin.Write(PinValue.High);
                DoNothing(ticksToWaitBetweenSteps[i]);

                _stepPin.Write(PinValue.Low);
                DoNothing(ticksToWaitBetweenSteps[i]);

                CurrentPosition += distanceToAddForOneStep;
            }

            _enablePin.Write(ENA_RELEASED);
        }

        private long[] WaitTimeToTicks(double[] waitTimeBetweenSteps)
        {
            long[] ticksToWaitBetweenSteps = new long[waitTimeBetweenSteps.Length];
            for (int i = 0; i < waitTimeBetweenSteps.Length; i++)
            {
                ticksToWaitBetweenSteps[i] = Convert.ToInt64(Math.Round((waitTimeBetweenSteps[i] / 1000) * Stopwatch.Frequency));
            }

            return ticksToWaitBetweenSteps;
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
