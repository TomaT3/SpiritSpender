using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpiritSpenderServer.Persistence
{
    public interface IDriveSettingRepository
    {
        // api/[GET]
        Task<IEnumerable<DriveSetting>> GetAllDriveSettings();
        // api/1/[GET]
        Task<DriveSetting> GetDriveSetting(string driveName);
        // api/[POST]
        Task Create(DriveSetting driveSetting);
        // api/[PUT]
        Task<bool> Update(DriveSetting driveSetting);
        // api/1/[DELETE]
        Task<bool> Delete(string driveName);
    }
}
