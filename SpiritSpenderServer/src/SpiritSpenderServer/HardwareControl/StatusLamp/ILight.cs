using UnitsNet;

namespace SpiritSpenderServer.HardwareControl.StatusLamp
{
    public interface ILight
    {
        void Blink(Duration durationOn, Duration durationOff);
        void TurnOff();
        void TurnOn();
    }
}