using SpiritSpenderServer.Persistence.Positions;
using System.Threading.Tasks;

namespace SpiritSpenderServer.Automatic
{
    public interface IAutomaticMode
    {
        Task DriveToPositionAsync(Position position);
        Task ReleaseTheSpiritAsync();
    }
}
