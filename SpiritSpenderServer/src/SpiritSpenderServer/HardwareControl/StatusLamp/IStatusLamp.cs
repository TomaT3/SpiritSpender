using SpiritSpenderServer.Persistence.StatusLampSettings;
using System.Threading.Tasks;
using UnitsNet;

namespace SpiritSpenderServer.HardwareControl.EmergencyStop
{
    public interface IStatusLamp
    {
        public StatusLampSetting StatusLampSetting { get; }
        public bool Enabled { get; }
        void EnableStatusLamp();
        void DisableStatusLamp();
        Task InitAsync();
        Task UpdateSettings(StatusLampSetting statusLampSetting);
        void GreenLightBlink();
        void GreenLightOff();
        void GreenLightOn();
        void RedLightBlink();
        void RedLightOff();
        void RedLightOn();
    }
}