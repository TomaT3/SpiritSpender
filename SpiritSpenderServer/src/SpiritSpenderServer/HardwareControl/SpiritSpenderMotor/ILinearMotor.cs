using System.Threading;
using System.Threading.Tasks;
using UnitsNet;

namespace SpiritSpenderServer.HardwareControl.SpiritSpenderMotor
{
    public interface ILinearMotor
    {
        Task DriveBackwardAsync(Duration drivingTime, CancellationToken token);
        Task DriveForwardAsync(Duration drivingTime, CancellationToken token);
    }
}
