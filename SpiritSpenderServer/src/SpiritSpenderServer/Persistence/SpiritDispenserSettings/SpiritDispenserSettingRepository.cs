using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpiritSpenderServer.Persistence.SpiritDispenserSettings
{
    public class SpiritDispenserSettingRepository : ISpiritDispenserSettingRepository
    {
        private readonly ISpiritSpenderDBContext _context;

        public SpiritDispenserSettingRepository(ISpiritSpenderDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SpiritDispenserSetting>> GetAllSpiritDispenserSettings()
        {
            return await _context
                            .SpiritDispenserSettings
                            .Find(_ => true)
                            .ToListAsync();
        }

        public Task<SpiritDispenserSetting> GetSpiritDispenserSetting(string name)
        {
            FilterDefinition<SpiritDispenserSetting> filter = Builders<SpiritDispenserSetting>.Filter.Eq(m => m.Name, name);
            return _context
                    .SpiritDispenserSettings
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }

        public async Task Create(SpiritDispenserSetting spiritDispenserSetting)
        {
            await _context.SpiritDispenserSettings.InsertOneAsync(spiritDispenserSetting);
        }

        public async Task<bool> Update(SpiritDispenserSetting spiritDispenserSetting)
        {
            ReplaceOneResult updateResult =
                await _context
                        .SpiritDispenserSettings
                        .ReplaceOneAsync(
                            filter: g => g.Name == spiritDispenserSetting.Name,
                            replacement: spiritDispenserSetting);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string name)
        {
            FilterDefinition<SpiritDispenserSetting> filter = Builders<SpiritDispenserSetting>.Filter.Eq(m => m.Name, name);
            DeleteResult deleteResult = await _context
                                                .SpiritDispenserSettings
                                              .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }
}
