using System;
using System.Linq;
using System.Threading.Tasks;

namespace SpiritSpenderServer.HardwareControl.SpiritSpenderMotor
{
    public interface ISpiritDispenserControl
    {
        string Name { get; }
        Task UpdateSettingsAsync();
        void FillGlas();
        void CloseSpiritSpender();
        void ReleaseSpirit();
        
    }
}
