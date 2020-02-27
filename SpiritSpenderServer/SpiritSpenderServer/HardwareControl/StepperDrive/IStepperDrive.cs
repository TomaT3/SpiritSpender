using System;
using System.Linq;
using System.Threading.Tasks;
using UnitsNet;

namespace SpiritSpenderServer.HardwareControl.StepperDrive
{
    public interface IStepperDrive
    {
        Length CurrentPosition { get; }
        void SetPosition(Length position);
        void DriveDistance(Length distance);
        void DriveToPosition(Length position);
        Task UpdateSettingsAsync();
    }
}
