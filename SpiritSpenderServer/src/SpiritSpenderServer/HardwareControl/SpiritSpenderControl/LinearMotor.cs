using SpiritSpenderServer.Helper;
using System;
using System.Device.Gpio;
using System.Threading;
using System.Threading.Tasks;
using UnitsNet;

namespace SpiritSpenderServer.HardwareControl.SpiritSpenderMotor
{
    public class LinearMotor : ILinearMotor
    {
        GpioPin _forwardPin;
        GpioPin _backwardPin;

        public LinearMotor(int forwardGpioPin, int backwardGpioPin, IGpioControllerFacade gpioControllerFacade)
            => InitGpio(forwardGpioPin, backwardGpioPin, gpioControllerFacade);


        private void InitGpio(int forwardGpioPin, int backwardGpioPin, IGpioControllerFacade gpioControllerFacade)
        {
            _forwardPin = new GpioPin(gpioControllerFacade, forwardGpioPin, PinMode.Output);
            _backwardPin = new GpioPin(gpioControllerFacade, backwardGpioPin, PinMode.Output);
            StopMotor();
        }

        public async Task<bool> DriveForwardAsync(Duration drivingTime, CancellationToken token)
        {
            DriveForward();
            var drivingSuccesfull = await Convert.ToInt32(drivingTime.Milliseconds).DelayExceptionFree(token);
            StopMotor();
            return drivingSuccesfull;
        }

        public async Task<bool> DriveBackwardAsync(Duration drivingTime, CancellationToken token)
        {
            DriveBackward();
            var drivingSuccesfull = await Convert.ToInt32(drivingTime.Milliseconds).DelayExceptionFree(token);
            StopMotor();
            return drivingSuccesfull;
        }

        private void DriveForward()
        {
            _backwardPin.Write(PinValue.Low);
            _forwardPin.Write(PinValue.High);
        }

        private void DriveBackward()
        {
            _backwardPin.Write(PinValue.High);
            _forwardPin.Write(PinValue.Low);
        }

        private void StopMotor()
        {
            _backwardPin.Write(PinValue.Low);
            _forwardPin.Write(PinValue.Low);
        }
    }
}
