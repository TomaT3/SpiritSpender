namespace SpiritSpenderServer.HardwareControl;

using SpiritSpenderServer.Interface.HardwareControl;
using System.Device.Gpio;

public class GpioPinFactory : IGpioPinFactory
{
    private readonly IGpioControllerFacade _gpioControllerFacade;

    public GpioPinFactory(IGpioControllerFacade controller)
    {
        _gpioControllerFacade = controller;
    }

    public IGpioPin CreateGpioPin(int pinNumber, PinMode pinMode)
    {
        return new GpioPin(_gpioControllerFacade, pinNumber, pinMode);
    }
}