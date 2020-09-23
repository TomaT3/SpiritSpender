using SpiritSpenderServer.HardwareControl;
using SpiritSpenderServer.Persistence.Positions;
using System.Threading.Tasks;

namespace SpiritSpenderServer.Automatic
{
    public interface IAutomaticMode : IStatus
    {
        Task DriveToPositionAsync(Position position);
        Task ReleaseTheSpiritAsync();
    }
}
