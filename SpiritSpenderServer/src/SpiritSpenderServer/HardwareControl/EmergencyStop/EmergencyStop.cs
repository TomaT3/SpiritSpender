using System;
using System.Device.Gpio;

namespace SpiritSpenderServer.HardwareControl.EmergencyStop
{
    public class EmergencyStop : IEmergencyStop
    {
        private GpioPin _emergencyStopReleased;

        public event Action<bool> EmergencyStopPressedChanged;

        public bool EmergencyStopPressed { get; private set; }

        public EmergencyStop(int emergencyStopGpioPin, IGpioControllerFacade gpioControllerFacade)
            => InitGpio(emergencyStopGpioPin, gpioControllerFacade);

        public void SetEmergencyStop(bool isEmergencyStopPressed)
        {
            EmergencyStopReleased_ValueChanged(isEmergencyStopPressed ? PinValue.Low : PinValue.High);
        }

        private void InitGpio(int emergencyStopGpioPin, IGpioControllerFacade gpioControllerFacade)
        {
            _emergencyStopReleased = new GpioPin(gpioControllerFacade, emergencyStopGpioPin, PinMode.Input);
            EmergencyStopReleased_ValueChanged(_emergencyStopReleased.Read());
            _emergencyStopReleased.ValueChanged += EmergencyStopReleased_ValueChanged;
        }

        private void EmergencyStopReleased_ValueChanged(PinValue pinValue)
        {
            EmergencyStopPressed = pinValue == PinValue.Low;
            EmergencyStopPressedChanged?.Invoke(EmergencyStopPressed);
        }
    }
}
