using MongoDB.Driver;
using SpiritSpenderServer.Config;
using SpiritSpenderServer.Persistence.Positions;
using SpiritSpenderServer.Persistence.DriveSettings;
using SpiritSpenderServer.Persistence.SpiritDispenserSettings;

namespace SpiritSpenderServer.Persistence
{
    public class SpiritSpenderDBContext : ISpiritSpenderDBContext
    {
        private readonly IMongoDatabase _db;
        public SpiritSpenderDBContext(MongoDBConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }

        public IMongoCollection<DriveSetting> DriveSettings => _db.GetCollection<DriveSetting>("DriveSettings");

        public IMongoCollection<SpiritDispenserSetting> SpiritDispenserSettings => _db.GetCollection<SpiritDispenserSetting>("SpiritDispenserSettings");

        public IMongoCollection<ShotGlassPositionSetting> ShotGlassPositionSettings => _db.GetCollection<ShotGlassPositionSetting>("ShotGlassPositionSettings");
    }
}
