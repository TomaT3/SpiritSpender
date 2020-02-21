using System;
using System.Linq;
using System.Threading.Tasks;

namespace SpiritSpenderServer.HardwareControl.SpiritSpenderMotor
{
    public interface ISpiritDispenserControl
    {
        Task UpdateSettings();
        void FillGlas();
        void CloseSpiritSpender();
        void ReleaseSpirit();
        
    }
}
