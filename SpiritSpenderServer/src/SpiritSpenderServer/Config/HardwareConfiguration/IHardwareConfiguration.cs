using SpiritSpenderServer.HardwareControl.EmergencyStop;
using SpiritSpenderServer.HardwareControl.SpiritSpenderMotor;
using SpiritSpenderServer.HardwareControl.StepperDrive;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpiritSpenderServer.Config.HardwareConfiguration
{
    public interface IHardwareConfiguration
    {
        Task LoadHardwareConfiguration();
        ISpiritDispenserControl SpiritDispenserControl { get; }
        Dictionary<string, IAxis> StepperDrives { get; }
        IEmergencyStop EmergencyStop { get; }
    }
}
