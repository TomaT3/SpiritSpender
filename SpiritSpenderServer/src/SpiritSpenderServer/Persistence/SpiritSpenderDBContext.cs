using MongoDB.Driver;
using SpiritSpenderServer.Config;
using SpiritSpenderServer.Persistence.Positions;
using SpiritSpenderServer.Persistence.DriveSettings;
using SpiritSpenderServer.Persistence.StatusLampSettings;
namespace SpiritSpenderServer.Persistence
{
    using SpiritSpenderServer.Config;
    using SpiritSpenderServer.Persistence.SpiritDispenserSettings;
    using Microsoft.Extensions.Options;

    public class SpiritSpenderDBContext : ISpiritSpenderDBContext
    {
        private readonly IMongoDatabase _db;
        public SpiritSpenderDBContext(IOptions<MongoDB> config)
        {
            var client = new MongoClient(config.Value.ConnectionString);
            _db = client.GetDatabase(config.Value.Database);
        }

        public IMongoCollection<DriveSetting> DriveSettings => _db.GetCollection<DriveSetting>("DriveSettings");

        public IMongoCollection<SpiritDispenserSetting> SpiritDispenserSettings => _db.GetCollection<SpiritDispenserSetting>("SpiritDispenserSettings");

        public IMongoCollection<ShotGlassPositionSetting> ShotGlassPositionSettings => _db.GetCollection<ShotGlassPositionSetting>("ShotGlassPositionSettings");

        public IMongoCollection<StatusLampSetting> StatusLampSettings => _db.GetCollection<StatusLampSetting>("StatusLampSettings");
    }
}
