namespace SpiritSpenderServer.Persistence.SpiritDispenserSettings;

public interface ISpiritDispenserSettingRepository
{
    Task<IEnumerable<SpiritDispenserSetting>> GetAllSpiritDispenserSettings();
    Task<SpiritDispenserSetting> GetSpiritDispenserSetting(string name);
    Task Create(SpiritDispenserSetting spiritDispenserSetting);
    Task<bool> Update(SpiritDispenserSetting spiritDispenserSetting);
    Task<bool> Delete(string name);
}
