using SpiritSpenderServer.Persistence.StatusLampSettings;
using System;
using System.Threading.Tasks;

namespace SpiritSpenderServer.HardwareControl.EmergencyStop
{
    public interface IStatusLamp
    {
        event Action<bool> EnabledChanged;
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