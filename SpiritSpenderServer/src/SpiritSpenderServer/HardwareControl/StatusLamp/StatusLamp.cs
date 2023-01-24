namespace SpiritSpenderServer.HardwareControl.StatusLamp;

using SpiritSpenderServer.Interface.HardwareControl;
using SpiritSpenderServer.Persistence.StatusLampSettings;
using UnitsNet;
using UnitsNet.Units;

public class StatusLamp : IStatusLamp
{
    private ILight _redLight;
    private ILight _greenLight;
    private IStatusLampSettingRepository _statusLampSettingRepository;
    private string _name;
    private bool _enabled = true;

    public event Action<bool>? EnabledChanged;

    public StatusLamp(IStatusLampSettingRepository statusLampSettingRepository, IGpioPinFactory gpioPinFactory)
    {
        _name = "StatusLamp";
        _statusLampSettingRepository = statusLampSettingRepository;
        _redLight = new Light(gpioPin: 19, gpioPinFactory: gpioPinFactory);
        _greenLight = new Light(gpioPin: 13, gpioPinFactory: gpioPinFactory);
    }

    public StatusLampSetting StatusLampSetting { get; private set; } = null!;

    public bool Enabled
    {
        get => _enabled;
        private set
        {
            if (value != _enabled)
            {
                _enabled = value;
                EnabledChanged?.Invoke(_enabled);
            }
        }
    }

    public async Task InitAsync()
    {
        StatusLampSetting = await _statusLampSettingRepository.GetStatusLampSetting(_name);
        if (StatusLampSetting == null)
        {
            StatusLampSetting = new StatusLampSetting
            {
                Name = _name,
                BlinkTimeOff = new Duration(0.5, DurationUnit.Second),
                BlinkTimeOn = new Duration(0.5, DurationUnit.Second),
            };

            await _statusLampSettingRepository.Create(StatusLampSetting);
        }
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
        _redLight.TurnOff();
        _greenLight.TurnOff();
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
