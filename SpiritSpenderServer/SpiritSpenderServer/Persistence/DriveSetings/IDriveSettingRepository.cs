using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpiritSpenderServer.Persistence.DriveSetings
{
    public interface IDriveSettingRepository
    {
        Task<IEnumerable<DriveSetting>> GetAllDriveSettings();
        Task<DriveSetting> GetDriveSetting(string driveName);
        Task Create(DriveSetting driveSetting);
        Task<bool> Update(DriveSetting driveSetting);
        Task<bool> Delete(string driveName);
    }
}
