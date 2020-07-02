using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Threading.Tasks;

namespace SpiritSpenderServer.HardwareControl
{
    public class GpioControllerFacade : IGpioControllerFacade
    {
        private GpioController _gpioController;

        public GpioControllerFacade() => _gpioController = new GpioController();

        public void OpenPin(int pinNumber, PinMode mode) => _gpioController.OpenPin(pinNumber, mode);

        public void ClosePin(int pinNumber) => _gpioController.ClosePin(pinNumber);

        public PinValue Read(int pinNumber) => _gpioController.Read(pinNumber);

        public void Write(int pinNumber, PinValue value) => _gpioController.Write(pinNumber, value);

        public void RegisterCallbackForPinValueChangedEvent(int pinNumber, PinEventTypes eventTypes, Action<PinValueChangedEventArgs> callback)
        {
            _gpioController.RegisterCallbackForPinValueChangedEvent(pinNumber, eventTypes, (sender, pinValueChangedEventArgs) => callback?.Invoke(pinValueChangedEventArgs));
        }

    }
}
