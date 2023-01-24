namespace SpiritSpenderServer.Interface.HardwareControl;
using System;
using System.Device.Gpio;

public interface IGpioPin
{
    event Action<PinValue> ValueChanged;

    PinValue Read();
    void Write(PinValue pinValue);
}