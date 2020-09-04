using SpiritSpenderServer.HardwareControl;
using SpiritSpenderServer.HardwareControl.EmergencyStop;
using SpiritSpenderServer.HardwareControl.SpiritSpenderMotor;
using SpiritSpenderServer.HardwareControl.StatusLamp;
using SpiritSpenderServer.Persistence.SpiritDispenserSettings;
using SpiritSpenderServer.Persistence.StatusLampSettings;
using System.Threading.Tasks;
using UnitsNet;
using UnitsNet.Units;

namespace SpiritSpenderServer.Config.HardwareConfiguration
{
    public class StatusLampConfiguration
    {
        public static async Task<StatusLamp> GetStatusLamp(IStatusLampSettingRepository statusLampSettingRepository, IGpioControllerFacade gpioControllerFacade)
        {
            const string STATUS_LAMP_NAME = "StatusLamp";
            var settings = await statusLampSettingRepository.GetStatusLampSetting(STATUS_LAMP_NAME);
            if (settings == null)
            {
                settings = new StatusLampSetting
                {
                    Name = STATUS_LAMP_NAME,
                    BlinkTimeOff = new Duration(0.5, DurationUnit.Second),
                    BlinkTimeOn = new Duration(0.5, DurationUnit.Second),
                };

                await statusLampSettingRepository.Create(settings);
            }

            var statusLamp = new StatusLamp(
                redLight: new Light(gpioPin: 19, gpioControllerFacade: gpioControllerFacade),
                greenLight: new Light(gpioPin: 13, gpioControllerFacade: gpioControllerFacade),
                statusLampSettingRepository, STATUS_LAMP_NAME);

            await statusLamp.InitAsync();

            return statusLamp;
           
        }
    }
}
