using SpiritSpenderServer.Persistence.Positions;
using System.Linq;
using System.Threading.Tasks;
using UnitsNet;
using UnitsNet.Units;

namespace SpiritSpenderServer.Config.HardwareConfiguration
{
    public class ShotGlassPositionSettingsConfiguration
    {
        public static async Task CreateShotGlassPositionSettings(IShotGlassPositionSettingRepository shotGlassPositionSettingRepository)
        {
            const int NUMBER_OF_SHOT_GLASS_POSITIONS = 12;
            var settings = await shotGlassPositionSettingRepository.GetAllSettingsAsync();
            
            if (settings != null && settings.Count() > 0)
                return;

            for (int i = 1; i <= NUMBER_OF_SHOT_GLASS_POSITIONS; i++)
            {
                await AddShotGlassPositionSettingAsync(i, shotGlassPositionSettingRepository);
            }
        }

        private static async Task AddShotGlassPositionSettingAsync(int positionNumber, IShotGlassPositionSettingRepository shotGlassPositionSettingRepository)
        {
            var shotGlasPosition = new ShotGlassPositionSetting
            {
                Number = positionNumber,
                Position = new Position { X = new Length(0, LengthUnit.Millimeter),
                                          Y = new Length(0, LengthUnit.Millimeter) },
                Quantity = Persistence.Positions.Quantity.Empty
            };

            await shotGlassPositionSettingRepository.CreateAsync(shotGlasPosition);
        }
    }
}
