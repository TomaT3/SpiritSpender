using SpiritSpenderServer.Interface.HardwareControl;
using System;
using System.Device.Gpio;

namespace SpiritSpenderServer.HardwareControl
{
    public class GpioPin : IGpioPin
    {
        private readonly IGpioControllerFacade _controller;
        private readonly int _pinNumber;

        public event Action<PinValue>? ValueChanged;

        public GpioPin(IGpioControllerFacade controller, int pinNumber, PinMode pinMode)
        {
            _controller = controller;
            _pinNumber = pinNumber;

            controller.OpenPin(_pinNumber, pinMode);
            if (pinMode == PinMode.Input)
            {
                _controller.RegisterCallbackForPinValueChangedEvent(pinNumber, PinEventTypes.Falling, ValueChangedHandler);
                _controller.RegisterCallbackForPinValueChangedEvent(pinNumber, PinEventTypes.Rising, ValueChangedHandler);

            }
        }

        public void Write(PinValue pinValue)
        {
            _controller.Write(_pinNumber, pinValue);
        }

        public PinValue Read()
        {
            return _controller.Read(_pinNumber);
        }

        private void ValueChangedHandler(PinValueChangedEventArgs pinValueChangedEventArgs)
        {
            var value = Read();
            ValueChanged?.Invoke(value);
        }
    }
}