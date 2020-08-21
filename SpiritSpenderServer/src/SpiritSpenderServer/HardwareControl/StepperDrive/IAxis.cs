using System.Threading.Tasks;
using UnitsNet;

namespace SpiritSpenderServer.HardwareControl.StepperDrive
{
    public interface IAxis
    {
        string DriveName { get; }
        Length CurrentPosition { get; }
        void SetPosition(Length position);
        Task DriveDistanceAsync(Length distance);
        Task DriveToPositionAsync(Length position);
        Task ReferenceDriveAsync();
        Task UpdateSettingsAsync();
    }
}
