using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Threading.Tasks;

namespace SpiritSpenderServer.HardwareControl
{
    public class GpioPin
    {
        private readonly GpioController _controller;
        private readonly int _pinNumber;
        public GpioPin(GpioController controller, int pinNumber, PinMode pinMode)
        {
            _controller = controller;
            _pinNumber = pinNumber;

            controller.OpenPin(_pinNumber, pinMode);
        }

        public void Write(PinValue pinValue)
        {
            _controller.Write(_pinNumber, pinValue);
        }

        public PinValue Read()
        {
            return _controller.Read(_pinNumber);
        }
    }
}