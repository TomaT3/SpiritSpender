using System.Threading.Tasks;

namespace SpiritSpenderServer.HardwareControl.SpiritSpenderMotor
{
    public interface ISpiritDispenserControl
    {
        string Name { get; }
        Task UpdateSettingsAsync();
        Task FillGlas();
        Task CloseSpiritSpender();
        Task ReleaseSpirit();
        
    }
}
