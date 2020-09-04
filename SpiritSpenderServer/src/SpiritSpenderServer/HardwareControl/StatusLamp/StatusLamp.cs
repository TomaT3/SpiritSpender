using SpiritSpenderServer.HardwareControl.StatusLamp;
using SpiritSpenderServer.Persistence.StatusLampSettings;
using System.Threading.Tasks;
using UnitsNet;

namespace SpiritSpenderServer.HardwareControl.EmergencyStop
{
    public class StatusLamp : IStatusLamp
    {
        private ILight _redLight;
        private ILight _greenLight;
        private IStatusLampSettingRepository _statusLampSettingRepository;
        private string _name;


        public StatusLamp(ILight redLight, ILight greenLight, IStatusLampSettingRepository statusLampSettingRepository, string name)
            => (_redLight, _greenLight, _statusLampSettingRepository, _name) = (redLight, greenLight, statusLampSettingRepository, name);

        public StatusLampSetting StatusLampSetting { get; private set; }


        public async Task InitAsync()
        {
            StatusLampSetting = await _statusLampSettingRepository.GetStatusLampSetting(_name);
        }

        public async Task UpdateSettings(StatusLampSetting statusLampSetting)
        {
            await _statusLampSettingRepository.Update(statusLampSetting);
            StatusLampSetting = await _statusLampSettingRepository.GetStatusLampSetting(_name);
        }

        public void GreenLightOn()
        {
            _greenLight.TurnOn();
        }

        public void GreenLightOff()
        {
            _greenLight.TurnOff();
        }

        public void GreenLightBlink()
        {
            _greenLight.Blink(StatusLampSetting.BlinkTimeOn, StatusLampSetting.BlinkTimeOff);
        }

        public void RedLightOn()
        {
            _redLight.TurnOn();
        }

        public void RedLightOff()
        {
            _redLight.TurnOff();
        }

        public void RedLightBlink()
        {
            _redLight.Blink(StatusLampSetting.BlinkTimeOn, StatusLampSetting.BlinkTimeOff);
        }
    }
}
