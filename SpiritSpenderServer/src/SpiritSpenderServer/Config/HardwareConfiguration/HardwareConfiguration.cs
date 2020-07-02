using SpiritSpenderServer.HardwareControl;
using SpiritSpenderServer.HardwareControl.EmergencyStop;
using SpiritSpenderServer.HardwareControl.SpiritSpenderMotor;
using SpiritSpenderServer.HardwareControl.StepperDrive;
using SpiritSpenderServer.Persistence.DriveSettings;
using SpiritSpenderServer.Persistence.Positions;
using SpiritSpenderServer.Persistence.SpiritDispenserSettings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpiritSpenderServer.Config.HardwareConfiguration
{
    public class HardwareConfiguration : IHardwareConfiguration
    {
        private IGpioControllerFacade _gpioControllerFacade;
        private ISpiritDispenserSettingRepository _spiritDispenserSettingRepository;
        private IDriveSettingRepository _driveSettingRepository;
        private IShotGlassPositionSettingRepository _shotGlassPositionSettingRepository;

        public HardwareConfiguration(ISpiritDispenserSettingRepository spiritDispenserSettingRepository, IDriveSettingRepository driveSettingRepository, IShotGlassPositionSettingRepository shotGlassPositionSettingRepository, IGpioControllerFacade gpioControllerFacade)
            => (_spiritDispenserSettingRepository, _driveSettingRepository, _shotGlassPositionSettingRepository, _gpioControllerFacade) 
             = (spiritDispenserSettingRepository, driveSettingRepository, shotGlassPositionSettingRepository, gpioControllerFacade);

        public IEmergencyStop EmergencyStop { get; private set; }

        public ISpiritDispenserControl SpiritDispenserControl { get; private set; }

        public Dictionary<string, IStepperDrive> StepperDrives { get; private set; }

        public async Task LoadHardwareConfiguration()
        {
            AddEmergencyStop();
            await AddSpiritDispenserControl();
            await AddDrives();
            await CreateShotGlassPositionsAsync();
        }

        private void AddEmergencyStop()
        {
            EmergencyStop = new EmergencyStop(12, _gpioControllerFacade);
        }

        private async Task AddSpiritDispenserControl()
        {
            SpiritDispenserControl = await SpiritDispenserConfiguration.GetSpiritDispenserControl(_spiritDispenserSettingRepository, _gpioControllerFacade);
            await SpiritDispenserControl.UpdateSettingsAsync();
        }

        private async Task AddDrives()
        {
            StepperDrives = new Dictionary<string, IStepperDrive>();
            await AddStepperDriveX();
            await AddStepperDriveY();
        }

        private async Task AddStepperDriveX()
        {
            var stepperDriveX = await DrivesConfiguration.GetStepperDriveX(_driveSettingRepository, _gpioControllerFacade);
            StepperDrives.Add(stepperDriveX.DriveName, stepperDriveX);
        }

        private async Task AddStepperDriveY()
        {
            var stepperDriveY = await DrivesConfiguration.GetStepperDriveY(_driveSettingRepository, _gpioControllerFacade);
            StepperDrives.Add(stepperDriveY.DriveName, stepperDriveY);
        }

        private async Task CreateShotGlassPositionsAsync()
        {
            await ShotGlassPositionSettingsConfiguration.CreateShotGlassPositionSettings(_shotGlassPositionSettingRepository);
        }
    }
}
