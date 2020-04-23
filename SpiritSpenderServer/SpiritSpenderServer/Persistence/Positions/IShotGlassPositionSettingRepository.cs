using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpiritSpenderServer.Persistence.Positions
{
    public interface IShotGlassPositionSettingRepository
    {
        Task CreateAsync(ShotGlassPositionSetting shotGlassPositionSetting);
        Task<bool> DeleteAsync(int positionNumber);
        Task<IEnumerable<ShotGlassPositionSetting>> GetAllSettingsAsync();
        Task<ShotGlassPositionSetting> GetSettingAsync(int positionNumber);
        Task<bool> UpdateAsync(ShotGlassPositionSetting shotGlassPositionSetting);
        Task<bool> UpdatePositionAsync(int positionNumber, Position position);
        Task<bool> UpdateQuantityAsync(int positionNumber, Quantity quantity);
    }
}
