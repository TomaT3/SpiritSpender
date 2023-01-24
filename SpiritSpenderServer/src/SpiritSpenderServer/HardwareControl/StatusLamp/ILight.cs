namespace SpiritSpenderServer.HardwareControl.StatusLamp;

using UnitsNet;

public interface ILight
{
    void Blink(Duration durationOn, Duration durationOff);
    void TurnOff();
    void TurnOn();
}