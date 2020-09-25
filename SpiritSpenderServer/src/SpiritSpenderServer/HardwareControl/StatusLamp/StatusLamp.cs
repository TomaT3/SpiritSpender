using SpiritSpenderServer.HardwareControl.StatusLamp;
using SpiritSpenderServer.Persistence.StatusLampSettings;
using System;
using System.Threading.Tasks;

namespace SpiritSpenderServer.HardwareControl.EmergencyStop
{
    public class StatusLamp : IStatusLamp
    {
        private ILight _redLight;
        private ILight _greenLight;
        private IStatusLampSettingRepository _statusLampSettingRepository;
        private string _name;
        private bool _enabled = true;

        public event Action<bool> EnabledChanged;

        public StatusLamp(ILight redLight, ILight greenLight, IStatusLampSettingRepository statusLampSettingRepository, string name)
            => (_redLight, _greenLight, _statusLampSettingRepository, _name) = (redLight, greenLight, statusLampSettingRepository, name);

        public StatusLampSetting StatusLampSetting { get; private set; }

        public bool Enabled { 
            get => _enabled;
            private set
            {
                if(value != _enabled)
                {
                    _enabled = value;
                    EnabledChanged?.Invoke(_enabled);
                }
            }
        }

        public async Task InitAsync()
        {
            StatusLampSetting = await _statusLampSettingRepository.GetStatusLampSetting(_name);
        }

        public async Task UpdateSettings(StatusLampSetting statusLampSetting)
        {
            await _statusLampSettingRepository.Update(statusLampSetting);
            StatusLampSetting = await _statusLampSettingRepository.GetStatusLampSetting(_name);
        }

        public void EnableStatusLamp()
        {
            Enabled = true;
        }

        public void DisableStatusLamp()
        {
            Enabled = false;
            RedLightOff();
            GreenLightOff();
        }

        public void GreenLightOn()
        {
            if (Enabled) _greenLight.TurnOn();
        }

        public void GreenLightOff()
        {
            if (Enabled) _greenLight.TurnOff();
        }

        public void GreenLightBlink()
        {
            if (Enabled) _greenLight.Blink(StatusLampSetting.BlinkTimeOn, StatusLampSetting.BlinkTimeOff);
        }

        public void RedLightOn()
        {
            if (Enabled) _redLight.TurnOn();
        }

        public void RedLightOff()
        {
            if (Enabled) _redLight.TurnOff();
        }

        public void RedLightBlink()
        {
            if (Enabled) _redLight.Blink(StatusLampSetting.BlinkTimeOn, StatusLampSetting.BlinkTimeOff);
        }
    }
}
