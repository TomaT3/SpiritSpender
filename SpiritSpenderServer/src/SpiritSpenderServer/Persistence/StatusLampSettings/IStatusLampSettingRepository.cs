namespace SpiritSpenderServer.Persistence.StatusLampSettings;

public interface IStatusLampSettingRepository
{
    Task<IEnumerable<StatusLampSetting>> GetStatusLampSettings();
    Task<StatusLampSetting> GetStatusLampSetting(string name);
    Task Create(StatusLampSetting settings);
    Task<bool> Update(StatusLampSetting settings);
    Task<bool> Delete(string name);
}
