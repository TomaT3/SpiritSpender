using System;
using SpiritSpenderServer.HardwareControl;
using SpiritSpenderServer.Persistence.Positions;
using System.Threading.Tasks;

namespace SpiritSpenderServer.Automatic
{
    public interface IAutomaticMode : IComponentWithStatus
    {
        event Action OneShotPoured;
        Task DriveToPositionAsync(Position position);
        Task ReleaseTheSpiritAsync();
        Task ReferenceAllAxis();
        Task GoToBottleChange();
    }
}
