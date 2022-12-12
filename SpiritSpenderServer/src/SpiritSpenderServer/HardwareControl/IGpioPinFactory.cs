using System.Device.Gpio;

namespace SpiritSpenderServer.HardwareControl
{
    public interface IGpioPinFactory
    {
        IGpioPin CreateGpioPin(int pinNumber, PinMode pinMode);
    }
}