namespace SpiritSpenderServer.Persistence.DriveSettings;

public interface IDriveSettingRepository
{
    Task<IEnumerable<string?>> GetAllDriveSettingNames();
    Task<IEnumerable<DriveSetting>> GetAllDriveSettings();
    Task<DriveSetting> GetDriveSetting(string driveName);
    Task Create(DriveSetting driveSetting);
    Task<bool> Update(DriveSetting driveSetting);
    Task<bool> Delete(string driveName);
}
