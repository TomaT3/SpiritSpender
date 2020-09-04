using SpiritSpenderServer.HardwareControl;
using SpiritSpenderServer.HardwareControl.EmergencyStop;
using SpiritSpenderServer.HardwareControl.SpiritSpenderMotor;
using SpiritSpenderServer.HardwareControl.StepperDrive;
using SpiritSpenderServer.Persistence.DriveSettings;
using SpiritSpenderServer.Persistence.Positions;
using SpiritSpenderServer.Persistence.SpiritDispenserSettings;
using SpiritSpenderServer.Persistence.StatusLampSettings;
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
        private IStatusLampSettingRepository _statusLampSettingRepository;

        public HardwareConfiguration(ISpiritDispenserSettingRepository spiritDispenserSettingRepository, IDriveSettingRepository driveSettingRepository, IShotGlassPositionSettingRepository shotGlassPositionSettingRepository, IStatusLampSettingRepository statusLampSettingRepository, IGpioControllerFacade gpioControllerFacade)
            => (_spiritDispenserSettingRepository, _driveSettingRepository, _shotGlassPositionSettingRepository, _statusLampSettingRepository, _gpioControllerFacade) 
             = (spiritDispenserSettingRepository, driveSettingRepository, shotGlassPositionSettingRepository, statusLampSettingRepository, gpioControllerFacade);

        public IEmergencyStop EmergencyStop { get; private set; }

        public IStatusLamp StatusLamp { get; private set; }

        public ISpiritDispenserControl SpiritDispenserControl { get; private set; }

        public Dictionary<string, IAxis> StepperDrives { get; private set; }

        public async Task LoadHardwareConfiguration()
        {
            AddEmergencyStop();
            await AddStatusLamp();
            await AddSpiritDispenserControl();
            await AddDrives();
            await CreateShotGlassPositionsAsync();
        }

        private void AddEmergencyStop()
        {
            EmergencyStop = new EmergencyStop(12, _gpioControllerFacade);
        }

        private async Task AddStatusLamp()
        {
            StatusLamp = await StatusLampConfiguration.GetStatusLamp(_statusLampSettingRepository, _gpioControllerFacade);
        }

        private async Task AddSpiritDispenserControl()
        {
            SpiritDispenserControl = await SpiritDispenserConfiguration.GetSpiritDispenserControl(_spiritDispenserSettingRepository, _gpioControllerFacade, EmergencyStop);
        }

        private async Task AddDrives()
        {
            StepperDrives = new Dictionary<string, IAxis>();
            await AddStepperDriveX();
            await AddStepperDriveY();
        }

        private async Task AddStepperDriveX()
        {
            var stepperDriveX = await DrivesConfiguration.GetStepperDriveX(_driveSettingRepository, _gpioControllerFacade, EmergencyStop);
            StepperDrives.Add(stepperDriveX.DriveSetting.DriveName, stepperDriveX);
        }

        private async Task AddStepperDriveY()
        {
            var stepperDriveY = await DrivesConfiguration.GetStepperDriveY(_driveSettingRepository, _gpioControllerFacade, EmergencyStop);
            StepperDrives.Add(stepperDriveY.DriveSetting.DriveName, stepperDriveY);
        }

        private async Task CreateShotGlassPositionsAsync()
        {
            await ShotGlassPositionSettingsConfiguration.CreateShotGlassPositionSettings(_shotGlassPositionSettingRepository);
        }
    }
}
