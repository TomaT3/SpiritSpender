using System;
using System.Device.Gpio;
using System.Linq;

namespace SpiritSpenderServer.HardwareControl
{
    public interface IGpioControllerFacade
    {
        void ClosePin(int pinNumber);
        void OpenPin(int pinNumber, PinMode mode);
        PinValue Read(int pinNumber);
        void Write(int pinNumber, PinValue value);
    }
}
