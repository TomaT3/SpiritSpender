using MongoDB.Driver;
using SpiritSpenderServer.Config;
using SpiritSpenderServer.Persistence.DriveSetings;
using SpiritSpenderServer.Persistence.SpiritDispenserSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
