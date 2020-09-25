using SpiritSpenderServer.Persistence.SpiritDispenserSettings;
using System.Threading.Tasks;

namespace SpiritSpenderServer.HardwareControl.SpiritSpenderMotor
{
    public interface ISpiritDispenserControl : IComponentWithStatus
    {
        SpiritDispenserSetting SpiritDispenserSetting { get; }
        SpiritDispenserPosition CurrentPosition { get; }
        Task InitAsync();
        Task UpdateSettingsAsync(SpiritDispenserSetting setting);
        Task ReferenceDriveAsync();
        Task GoToBottleChangePosition();
        Task FillGlas();
        Task CloseSpiritSpender();
        Task ReleaseSpirit();
        
    }
}
