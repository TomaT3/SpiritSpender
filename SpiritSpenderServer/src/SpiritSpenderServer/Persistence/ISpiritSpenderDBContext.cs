using MongoDB.Driver;
using SpiritSpenderServer.Persistence.Positions;
using SpiritSpenderServer.Persistence.DriveSettings;
using SpiritSpenderServer.Persistence.SpiritDispenserSettings;
using SpiritSpenderServer.Persistence.StatusLampSettings;

namespace SpiritSpenderServer.Persistence
{
    public interface ISpiritSpenderDBContext
    {
        IMongoCollection<DriveSetting> DriveSettings { get; }
        IMongoCollection<SpiritDispenserSetting> SpiritDispenserSettings { get; }
        IMongoCollection<ShotGlassPositionSetting> ShotGlassPositionSettings { get; }
        IMongoCollection<StatusLampSetting> StatusLampSettings { get; }
    }
}
