using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpiritSpenderServer.Persistence.StatusLampSettings
{
    public class StatusLampSettingRepository : IStatusLampSettingRepository
    {
        private readonly ISpiritSpenderDBContext _context;

        public StatusLampSettingRepository(ISpiritSpenderDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StatusLampSetting>> GetStatusLampSettings()
        {
            return await _context
                            .StatusLampSettings
                            .Find(_ => true)
                            .ToListAsync();
        }

        public Task<StatusLampSetting> GetStatusLampSetting(string name)
        {
            FilterDefinition<StatusLampSetting> filter = Builders<StatusLampSetting>.Filter.Eq(m => m.Name, name);
            return _context
                    .StatusLampSettings
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }

        public async Task Create(StatusLampSetting setting)
        {
            await _context.StatusLampSettings.InsertOneAsync(setting);
        }

        public async Task<bool> Update(StatusLampSetting setting)
        {
            ReplaceOneResult updateResult =
                await _context
                        .StatusLampSettings
                        .ReplaceOneAsync(
                            filter: g => g.Name == setting.Name,
                            replacement: setting);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string name)
        {
            FilterDefinition<StatusLampSetting> filter = Builders<StatusLampSetting>.Filter.Eq(m => m.Name, name);
            DeleteResult deleteResult = await _context
                                              .StatusLampSettings
                                              .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }
}
