﻿using System;
using System.Device.Gpio;

namespace SpiritSpenderServer.HardwareControl
{
    public interface IGpioControllerFacade
    {
        void ClosePin(int pinNumber);
        void OpenPin(int pinNumber, PinMode mode);
        PinValue Read(int pinNumber);
        void Write(int pinNumber, PinValue value);
        void RegisterCallbackForPinValueChangedEvent(int pinNumber, PinEventTypes eventTypes, Action<PinValueChangedEventArgs> callback);
    }
}
