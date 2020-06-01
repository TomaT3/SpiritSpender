using SpiritSpenderServer.HardwareControl;
using SpiritSpenderServer.HardwareControl.SpiritSpenderMotor;
using SpiritSpenderServer.Persistence.SpiritDispenserSettings;
using System.Threading.Tasks;
using UnitsNet;
using UnitsNet.Units;

namespace SpiritSpenderServer.Config.HardwareConfiguration
{
    public class SpiritDispenserConfiguration
    {
        public static async Task<SpiritDispenserControl> GetSpiritDispenserControl(ISpiritDispenserSettingRepository spiritDispenserSettingRepository, IGpioControllerFacade gpioControllerFacade)
        {
            const string SPIRIT_DISPENSER_NAME = "SpiritDispenser";
            var settings = await spiritDispenserSettingRepository.GetSpiritDispenserSetting(SPIRIT_DISPENSER_NAME);
            if (settings == null)
            {
                settings = new SpiritDispenserSetting
                {
                    Name = SPIRIT_DISPENSER_NAME,
                    DriveTimeToCloseTheSpiritSpender = new Duration(1, DurationUnit.Second),
                    DriveTimeToReleaseTheSpirit = new Duration(1.5, DurationUnit.Second),
                    WaitTimeUntilSpiritIsReleased = new Duration(1.8, DurationUnit.Second),
                    WaitTimeUntilSpiritIsRefilled = new Duration(1.5, DurationUnit.Second)
                };

                await spiritDispenserSettingRepository.Create(settings);
            }

            return new SpiritDispenserControl(
                new SpiritSpenderMotor(forwardGpioPin: 18, backwardGpioPin: 23, gpioControllerFacade: gpioControllerFacade),
                spiritDispenserSettingRepository, SPIRIT_DISPENSER_NAME);
           
        }
    }
}
