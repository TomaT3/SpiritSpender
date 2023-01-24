namespace SpiritSpenderServer.Automatic;

using SpiritSpenderServer.HardwareControl;
using SpiritSpenderServer.Persistence.Positions;

public interface IAutomaticMode : IComponentWithStatus
{
    event Action OneShotPoured;
    Task DriveToPositionAsync(Position position);
    Task ReleaseTheSpiritAsync();
    Task ReferenceAllAxis();
    Task GoToBottleChange();
}
