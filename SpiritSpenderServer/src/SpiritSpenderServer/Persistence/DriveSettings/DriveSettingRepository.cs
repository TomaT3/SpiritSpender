using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpiritSpenderServer.Persistence.DriveSettings
{
    class DriveSettingRepository : IDriveSettingRepository
    {
        private readonly ISpiritSpenderDBContext _context;
        public DriveSettingRepository(ISpiritSpenderDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DriveSetting>> GetAllDriveSettings()
        {
            return await _context
                            .DriveSettings
                            .Find(_ => true)
                            .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetAllDriveSettingNames()
        {
            return await _context
                            .DriveSettings
                            .Find(_ => true)
                            .Project(ds => ds.DriveName)
                            .ToListAsync();
        }

        public Task<DriveSetting> GetDriveSetting(string driveName)
        {
            FilterDefinition<DriveSetting> filter = Builders<DriveSetting>.Filter.Eq(m => m.DriveName, driveName);
            return _context
                    .DriveSettings
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }

        public async Task Create(DriveSetting driveSetting)
        {
            await _context.DriveSettings.InsertOneAsync(driveSetting);
        }

        public async Task<bool> Update(DriveSetting driveSetting)
        {
            ReplaceOneResult updateResult =
                await _context
                        .DriveSettings
                        .ReplaceOneAsync(
                            filter: g => g.DriveName == driveSetting.DriveName,
                            replacement: driveSetting);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string driveName)
        {
            FilterDefinition<DriveSetting> filter = Builders<DriveSetting>.Filter.Eq(m => m.DriveName, driveName);
            DeleteResult deleteResult = await _context
                                                .DriveSettings
                                              .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }
}
