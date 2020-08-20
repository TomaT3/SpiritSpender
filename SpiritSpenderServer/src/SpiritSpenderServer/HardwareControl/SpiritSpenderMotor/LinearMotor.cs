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
        private CancellationTokenSource _cancelMovementTokensource;
        private object _lockObject = new Object();

        public LinearMotor(int forwardGpioPin, int backwardGpioPin, IGpioControllerFacade gpioControllerFacade)
            => InitGpio(forwardGpioPin, backwardGpioPin, gpioControllerFacade);


        private void InitGpio(int forwardGpioPin, int backwardGpioPin, IGpioControllerFacade gpioControllerFacade)
        {
            _cancelMovementTokensource = new CancellationTokenSource();
            _forwardPin = new GpioPin(gpioControllerFacade, forwardGpioPin, PinMode.Output);
            _backwardPin = new GpioPin(gpioControllerFacade, backwardGpioPin, PinMode.Output);
            StopMotor();
        }

        public async Task DriveForward(Duration drivingTime)
        {
            DriveForward();
            await Task.Delay(Convert.ToInt32(drivingTime.Milliseconds), _cancelMovementTokensource.Token);
            StopMovement();
        }

        public async Task DriveBackward(Duration drivingTime)
        {
            DriveBackward();
            await Task.Delay(Convert.ToInt32(drivingTime.Milliseconds), _cancelMovementTokensource.Token);
            StopMovement();
        }

        public void StopMovement()
        {
            StopMotor();

            lock (_lockObject)
            {
                _cancelMovementTokensource.Cancel();
                _cancelMovementTokensource = new CancellationTokenSource();
            }
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
