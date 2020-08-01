using UnitsNet;

namespace SpiritSpenderServer.HardwareControl.EmergencyStop
{
    public interface IStatusLamp
    {
        void GreenLightBlink(Duration durationOn, Duration durationOff);
        void GreenLightOff();
        void GreenLightOn();
        void RedLightBlink(Duration durationOn, Duration durationOff);
        void RedLightOff();
        void RedLightOn();
    }
}