namespace SpiritSpenderServer.Persistence.Positions;

using MongoDB.Driver;

class ShotGlassPositionSettingRepository : IShotGlassPositionSettingRepository
{
    private readonly ISpiritSpenderDBContext _context;
    public ShotGlassPositionSettingRepository(ISpiritSpenderDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ShotGlassPositionSetting>> GetAllSettingsAsync()
    {
        return await _context
                        .ShotGlassPositionSettings
                        .Find(_ => true)
                        .ToListAsync();
    }

    public Task<ShotGlassPositionSetting> GetSettingAsync(int positionNumber)
    {
        FilterDefinition<ShotGlassPositionSetting> filter = Builders<ShotGlassPositionSetting>.Filter.Eq(m => m.Number, positionNumber);
        return _context
                .ShotGlassPositionSettings
                .Find(filter)
                .FirstOrDefaultAsync();
    }

    public async Task<long> GetCountAsync()
    {
        return await _context.ShotGlassPositionSettings.CountDocumentsAsync(s => s.Number > 0);
    }

    public async Task CreateAsync(ShotGlassPositionSetting shotGlassPositionSetting)
    {
        await _context.ShotGlassPositionSettings.InsertOneAsync(shotGlassPositionSetting);
    }

    public async Task<bool> UpdatePositionAsync(int positionNumber, Position position)
    {
        var updateResult =
            await _context
                    .ShotGlassPositionSettings
                    .UpdateOneAsync<ShotGlassPositionSetting>(
                f => f.Number == positionNumber,
                Builders<ShotGlassPositionSetting>.Update.Set(f => f.Position, position));

        return updateResult.IsAcknowledged
                && updateResult.ModifiedCount > 0;
    }

    public async Task<bool> UpdateQuantityAsync(int positionNumber, Quantity quantity)
    {
        var updateResult =
            await _context
                    .ShotGlassPositionSettings
                    .UpdateOneAsync<ShotGlassPositionSetting>(
                f => f.Number == positionNumber,
                Builders<ShotGlassPositionSetting>.Update.Set(f => f.Quantity, quantity));

        return updateResult.IsAcknowledged
                && updateResult.ModifiedCount > 0;
    }

    public async Task<bool> ClearQuantityAsync()
    {
        var updateResult =
            await _context
                    .ShotGlassPositionSettings
                    .UpdateManyAsync<ShotGlassPositionSetting>(
                f => true,
                Builders<ShotGlassPositionSetting>.Update.Set(f => f.Quantity, Quantity.Empty));

        return updateResult.IsAcknowledged
            && updateResult.ModifiedCount > 0;
    }

    public async Task<bool> UpdateAsync(ShotGlassPositionSetting shotGlassPositionSetting)
    {
        ReplaceOneResult updateResult =
            await _context
                    .ShotGlassPositionSettings
                    .ReplaceOneAsync(
                        filter: g => g.Number == shotGlassPositionSetting.Number,
                        replacement: shotGlassPositionSetting);
        return updateResult.IsAcknowledged
                && updateResult.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(int positionNumber)
    {
        FilterDefinition<ShotGlassPositionSetting> filter = Builders<ShotGlassPositionSetting>.Filter.Eq(m => m.Number, positionNumber);
        DeleteResult deleteResult = await _context
                                            .ShotGlassPositionSettings
                                            .DeleteOneAsync(filter);
        return deleteResult.IsAcknowledged
            && deleteResult.DeletedCount > 0;
    }
}
