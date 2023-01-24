namespace SpiritSpenderServer.Persistence;

using MongoDB.Driver;
using SpiritSpenderServer.Persistence.DriveSettings;
using SpiritSpenderServer.Persistence.Positions;
using SpiritSpenderServer.Persistence.SpiritDispenserSettings;
using SpiritSpenderServer.Persistence.StatusLampSettings;

public interface ISpiritSpenderDBContext
{
    IMongoCollection<DriveSetting> DriveSettings { get; }
    IMongoCollection<SpiritDispenserSetting> SpiritDispenserSettings { get; }
    IMongoCollection<ShotGlassPositionSetting> ShotGlassPositionSettings { get; }
    IMongoCollection<StatusLampSetting> StatusLampSettings { get; }
}
