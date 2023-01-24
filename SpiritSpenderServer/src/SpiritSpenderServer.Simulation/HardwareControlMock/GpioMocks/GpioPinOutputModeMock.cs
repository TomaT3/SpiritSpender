using SpiritSpenderServer.Interface.HardwareControl;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritSpenderServer.Simulation.HardwareControlMock.GpioMocks;

public class GpioPinOutputModeMock : IGpioPin
{
    private PinValue _currentPinValue;
    public event Action<PinValue> ValueChanged;

    public PinValue Read()
    {
        return _currentPinValue;
    }

    public void Write(PinValue pinValue)
    {
        _currentPinValue = pinValue;
        ValueChanged?.Invoke(_currentPinValue);
    }
}
