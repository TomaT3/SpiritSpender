﻿namespace SpiritSpenderServer.Simulation.HardwareControlMock.GpioMocks;
using SpiritSpenderServer.Interface.HardwareControl;
using System;
using System.Device.Gpio;

public class GpioPinInputModeMock : IGpioPin
{
    private PinValue _currentPinValue;
    public event Action<PinValue> ValueChanged;

    internal GpioPinInputModeMock(PinValue initialPinValue)
    {
        _currentPinValue = initialPinValue;
    }

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
