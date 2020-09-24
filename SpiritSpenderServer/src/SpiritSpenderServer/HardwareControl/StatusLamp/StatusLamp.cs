using SpiritSpenderServer.HardwareControl.StatusLamp;
using SpiritSpenderServer.Persistence.StatusLampSettings;
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


        public StatusLamp(ILight redLight, ILight greenLight, IStatusLampSettingRepository statusLampSettingRepository, string name)
            => (_redLight, _greenLight, _statusLampSettingRepository, _name) = (redLight, greenLight, statusLampSettingRepository, name);

        public StatusLampSetting StatusLampSetting { get; private set; }
        public bool Enabled { get => _enabled; }

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
            _enabled = true;
        }

        public void DisableStatusLamp()
        {
            _enabled = false;
            RedLightOff();
            GreenLightOff();
        }

        public void GreenLightOn()
        {
            if (_enabled) _greenLight.TurnOn();
        }

        public void GreenLightOff()
        {
            if (_enabled) _greenLight.TurnOff();
        }

        public void GreenLightBlink()
        {
            if (_enabled) _greenLight.Blink(StatusLampSetting.BlinkTimeOn, StatusLampSetting.BlinkTimeOff);
        }

        public void RedLightOn()
        {
            if (_enabled) _redLight.TurnOn();
        }

        public void RedLightOff()
        {
            if (_enabled) _redLight.TurnOff();
        }

        public void RedLightBlink()
        {
            if (_enabled) _redLight.Blink(StatusLampSetting.BlinkTimeOn, StatusLampSetting.BlinkTimeOff);
        }
    }
}
