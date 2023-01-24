namespace SpiritSpenderServer.Interface.HardwareControl;
using System.Device.Gpio;

public interface IGpioPinFactory
{
    IGpioPin CreateGpioPin(int pinNumber, PinMode pinMode);
}