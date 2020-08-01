using UnitsNet;

namespace SpiritSpenderServer.HardwareControl.SpiritSpenderMotor
{
    public interface ISpiritSpenderMotor
    {
        void DriveBackward(Duration drivingTime);
        void DriveForward(Duration drivingTime);
    }
}
