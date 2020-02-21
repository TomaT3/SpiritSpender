using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnitsNet;

namespace SpiritSpenderServer.HardwareControl.SpiritSpenderMotor
{
    public class SpiritSpenderMotor : ISpiritSpenderMotor
    {
        GpioPin _forwardPin;
        GpioPin _backwardPin;

        public SpiritSpenderMotor()
        {
            InitGpio();
        }

        private void InitGpio()
        {
            GpioController controller = new GpioController();
            _forwardPin = new GpioPin(controller, 18, PinMode.Output);
            _backwardPin = new GpioPin(controller, 23, PinMode.Output);
            StopMotor();
        }

        public void DriveForward(Duration drivingTime)
        {
            DriveForward();
            Thread.Sleep(Convert.ToInt32(drivingTime.Milliseconds));
            StopMotor();
        }

        public void DriveBackward(Duration drivingTime)
        {
            DriveBackward();
            Thread.Sleep(Convert.ToInt32(drivingTime.Milliseconds));
            StopMotor();
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
