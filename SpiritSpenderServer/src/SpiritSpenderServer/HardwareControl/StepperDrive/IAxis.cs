using SpiritSpenderServer.Persistence.DriveSettings;
using System.Threading.Tasks;
using UnitsNet;

namespace SpiritSpenderServer.HardwareControl.StepperDrive
{
    public interface IAxis : IComponentWithStatus
    {
        DriveSetting DriveSetting { get; }
        Length CurrentPosition { get; }
        Task InitAsync();
        Task UpdateSettingsAsync(DriveSetting setting);
        void SetPosition(Length position);
        Task DriveDistanceAsync(Length distance);
        Task DriveToPositionAsync(Length position);
        Task ReferenceDriveAsync();
    }
}
