using SpiritSpenderServer.HardwareControl.StatusLamp;
using UnitsNet;

namespace SpiritSpenderServer.HardwareControl.EmergencyStop
{
    public class StatusLamp : IStatusLamp
    {
        private ILight _redLight;
        private ILight _greenLight;


        public StatusLamp(ILight redLight, ILight greenLight)
            => (_redLight, _greenLight) = (redLight, greenLight);

        public void GreenLightOn()
        {
            _greenLight.TurnOn();
        }

        public void GreenLightOff()
        {
            _greenLight.TurnOff();
        }

        public void GreenLightBlink(Duration durationOn, Duration durationOff)
        {
            _greenLight.Blink(durationOn, durationOff);
        }

        public void RedLightOn()
        {
            _redLight.TurnOn();
        }

        public void RedLightOff()
        {
            _redLight.TurnOff();
        }

        public void RedLightBlink(Duration durationOn, Duration durationOff)
        {
            _redLight.Blink(durationOn, durationOff);
        }
    }
}
