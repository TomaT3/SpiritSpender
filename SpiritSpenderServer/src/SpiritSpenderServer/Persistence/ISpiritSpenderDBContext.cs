using MongoDB.Driver;
using SpiritSpenderServer.Persistence.Positions;
using SpiritSpenderServer.Persistence.DriveSettings;
using SpiritSpenderServer.Persistence.SpiritDispenserSettings;

namespace SpiritSpenderServer.Persistence
{
    public interface ISpiritSpenderDBContext
    {
        IMongoCollection<DriveSetting> DriveSettings { get; }
        IMongoCollection<SpiritDispenserSetting> SpiritDispenserSettings { get; }
        IMongoCollection<ShotGlassPositionSetting> ShotGlassPositionSettings { get; }
    }
}
