using System.Threading.Tasks;

namespace SpiritSpenderServer.HardwareControl.SpiritSpenderMotor
{
    public interface ISpiritDispenserControl
    {
        string Name { get; }
        Status Status { get; }
        SpiritDispenserPosition CurrentPosition { get; }
        Task UpdateSettingsAsync();
        Task ReferenceDriveAsync();
        Task GoToBottleChangePosition();
        Task FillGlas();
        Task CloseSpiritSpender();
        Task ReleaseSpirit();
        
    }
}
