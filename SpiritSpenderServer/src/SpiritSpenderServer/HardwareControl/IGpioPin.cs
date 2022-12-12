using System;
using System.Device.Gpio;

namespace SpiritSpenderServer.HardwareControl
{
    public interface IGpioPin
    {
        event Action<PinValue> ValueChanged;

        PinValue Read();
        void Write(PinValue pinValue);
    }
}