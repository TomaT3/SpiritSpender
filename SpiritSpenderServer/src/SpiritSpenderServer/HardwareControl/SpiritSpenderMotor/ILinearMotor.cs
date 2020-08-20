using System.Threading.Tasks;
using UnitsNet;

namespace SpiritSpenderServer.HardwareControl.SpiritSpenderMotor
{
    public interface ILinearMotor
    {
        Task DriveBackward(Duration drivingTime);
        Task DriveForward(Duration drivingTime);
        void StopMovement();
    }
}
