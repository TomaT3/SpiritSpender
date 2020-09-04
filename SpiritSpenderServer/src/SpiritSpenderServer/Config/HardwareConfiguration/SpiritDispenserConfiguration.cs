using SpiritSpenderServer.HardwareControl;
using SpiritSpenderServer.HardwareControl.EmergencyStop;
using SpiritSpenderServer.HardwareControl.SpiritSpenderMotor;
using SpiritSpenderServer.Persistence.SpiritDispenserSettings;
using System.Threading.Tasks;
using UnitsNet;
using UnitsNet.Units;

namespace SpiritSpenderServer.Config.HardwareConfiguration
{
    public class SpiritDispenserConfiguration
    {
        public static async Task<SpiritDispenserControl> GetSpiritDispenserControl(ISpiritDispenserSettingRepository spiritDispenserSettingRepository, IGpioControllerFacade gpioControllerFacade, IEmergencyStop emergencyStop)
        {
            const string SPIRIT_DISPENSER_NAME = "SpiritDispenser";
            var settings = await spiritDispenserSettingRepository.GetSpiritDispenserSetting(SPIRIT_DISPENSER_NAME);
            if (settings == null)
            {
                settings = new SpiritDispenserSetting
                {
                    Name = SPIRIT_DISPENSER_NAME,
                    DriveTimeFromBottleChangeToHomePos = new Duration(1.4, DurationUnit.Second),
                    DriveTimeFromHomePosToBottleChange = new Duration(2, DurationUnit.Second),
                    DriveTimeFromReleaseToHomePosition = new Duration(1, DurationUnit.Second),
                    DriveTimeFromHomeToReleasePosition = new Duration(1.5, DurationUnit.Second),
                    WaitTimeUntilSpiritIsReleased = new Duration(1.8, DurationUnit.Second),
                    WaitTimeUntilSpiritIsRefilled = new Duration(1.5, DurationUnit.Second)
                };

                await spiritDispenserSettingRepository.Create(settings);
            }

            var spiritDispenserControl = new SpiritDispenserControl(
                new LinearMotor(forwardGpioPin: 18, backwardGpioPin: 23, gpioControllerFacade: gpioControllerFacade),
                spiritDispenserSettingRepository, emergencyStop, SPIRIT_DISPENSER_NAME);
            await spiritDispenserControl.InitAsync();

            return spiritDispenserControl;
           
        }
    }
}
