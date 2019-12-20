using System;
using System.Linq;
using System.Threading.Tasks;
using UnitsNet;

namespace SpiritSpenderServer.HardwareControl
{
    public interface IStepperDrive
    {
        Length CurrentPosition { get; }

        void DriveDistance(Length distance);
        void DriveToPosition(Length position);
        Task InitDriveAsync();
    }
}
