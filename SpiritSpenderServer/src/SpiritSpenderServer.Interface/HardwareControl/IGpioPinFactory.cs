using System.Device.Gpio;

namespace SpiritSpenderServer.Interface.HardwareControl
{
    public interface IGpioPinFactory
    {
        IGpioPin CreateGpioPin(int pinNumber, PinMode pinMode);
    }
}