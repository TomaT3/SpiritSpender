using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

        public StepperMotorControl(int enablePin, int directionPin, int stepPin)
        {
            GpioController controller = new GpioController();
            _enablePin = new GpioPin(controller, enablePin, PinMode.Output);
            _enablePin.Write(ENA_RELEASED);

            _directionPin = new GpioPin(controller, directionPin, PinMode.Output);
            _directionPin.Write(BACKWARD);

            _stepPin = new GpioPin(controller, stepPin, PinMode.Output);
            _stepPin.Write(PinValue.Low);
        }

        public Length CurrentPosition { get; private set; }

        public void SetOutput(double[] waitTimeBetweenSteps, bool direction, Length distanceToAddForOneStep)
        {
            _enablePin.Write(ENA_LOCKED);
            _directionPin.Write(direction ? FORWARD : BACKWARD);

            DoNothing(500);

            for (int i = 0; i < waitTimeBetweenSteps.Length; i++)
            {
                _stepPin.Write(PinValue.High);
                DoNothing(waitTimeBetweenSteps[i]);

                _stepPin.Write(PinValue.Low);
                DoNothing(waitTimeBetweenSteps[i]);

                CurrentPosition += distanceToAddForOneStep;
            }

            _enablePin.Write(ENA_RELEASED);
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
