using System;

namespace SpiritSpenderServer.HardwareControl.EmergencyStop
{
    public interface IEmergencyStop
    {
        bool EmergencyStopPressed { get; }

        event Action<bool> EmergencyStopPressedChanged;
        void SetDebounceTime(int debounceTime);
        void SetEmergencyStop(bool isEmergencyStopPressed);
    }
}