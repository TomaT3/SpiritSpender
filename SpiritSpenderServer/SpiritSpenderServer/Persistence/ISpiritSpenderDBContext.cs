using MongoDB.Driver;
using SpiritSpenderServer.Persistence.Positions;
using SpiritSpenderServer.Persistence.DriveSettings;
using SpiritSpenderServer.Persistence.SpiritDispenserSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpiritSpenderServer.Persistence
{
    public interface ISpiritSpenderDBContext
    {
        IMongoCollection<DriveSetting> DriveSettings { get; }
        IMongoCollection<SpiritDispenserSetting> SpiritDispenserSettings { get; }
        IMongoCollection<ShotGlassPositionSetting> ShotGlassPositionSettings { get; }
    }
}
