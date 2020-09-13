using SpiritSpenderServer.Persistence.Positions;
using System;
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
            var shotGlasPosition = GetInitialShotGlassPositionSetting(positionNumber);
            await shotGlassPositionSettingRepository.CreateAsync(shotGlasPosition);
        }

        private static ShotGlassPositionSetting GetInitialShotGlassPositionSetting(int positionNumber)
        {
            switch (positionNumber)
            {
                case 1:
                    return new ShotGlassPositionSetting
                    {
                        Number = 1,
                        Position = new Position
                        {
                            X = new Length(12, LengthUnit.Millimeter),
                            Y = new Length(2, LengthUnit.Millimeter)
                        },
                        Quantity = Persistence.Positions.Quantity.Empty
                    };

                case 2:
                    return new ShotGlassPositionSetting
                    {
                        Number = 2,
                        Position = new Position
                        {
                            X = new Length(82.7, LengthUnit.Millimeter),
                            Y = new Length(2, LengthUnit.Millimeter)
                        },
                        Quantity = Persistence.Positions.Quantity.Empty
                    };

                case 3:
                    return new ShotGlassPositionSetting
                    {
                        Number = 3,
                        Position = new Position
                        {
                            X = new Length(158.3, LengthUnit.Millimeter),
                            Y = new Length(2, LengthUnit.Millimeter)
                        },
                        Quantity = Persistence.Positions.Quantity.Empty
                    };

                case 4:
                    return new ShotGlassPositionSetting
                    {
                        Number = 4,
                        Position = new Position
                        {
                            X = new Length(232.3, LengthUnit.Millimeter),
                            Y = new Length(2, LengthUnit.Millimeter)
                        },
                        Quantity = Persistence.Positions.Quantity.Empty
                    };

                case 5:
                    return new ShotGlassPositionSetting
                    {
                        Number = 5,
                        Position = new Position
                        {
                            X = new Length(12, LengthUnit.Millimeter),
                            Y = new Length(71, LengthUnit.Millimeter)
                        },
                        Quantity = Persistence.Positions.Quantity.Empty
                    };

                case 6:
                    return new ShotGlassPositionSetting
                    {
                        Number = 6,
                        Position = new Position
                        {
                            X = new Length(84, LengthUnit.Millimeter),
                            Y = new Length(72, LengthUnit.Millimeter)
                        },
                        Quantity = Persistence.Positions.Quantity.Empty
                    };

                case 7:
                    return new ShotGlassPositionSetting
                    {
                        Number = 7,
                        Position = new Position
                        {
                            X = new Length(162, LengthUnit.Millimeter),
                            Y = new Length(70, LengthUnit.Millimeter)
                        },
                        Quantity = Persistence.Positions.Quantity.Empty
                    };

                case 8:
                    return new ShotGlassPositionSetting
                    {
                        Number = 8,
                        Position = new Position
                        {
                            X = new Length(236, LengthUnit.Millimeter),
                            Y = new Length(72, LengthUnit.Millimeter)
                        },
                        Quantity = Persistence.Positions.Quantity.Empty
                    };

                case 9:
                    return new ShotGlassPositionSetting
                    {
                        Number = 9,
                        Position = new Position
                        {
                            X = new Length(12, LengthUnit.Millimeter),
                            Y = new Length(139, LengthUnit.Millimeter)
                        },
                        Quantity = Persistence.Positions.Quantity.Empty
                    };

                case 10:
                    return new ShotGlassPositionSetting
                    {
                        Number = 10,
                        Position = new Position
                        {
                            X = new Length(88, LengthUnit.Millimeter),
                            Y = new Length(139, LengthUnit.Millimeter)
                        },
                        Quantity = Persistence.Positions.Quantity.Empty
                    };

                case 11:
                    return new ShotGlassPositionSetting
                    {
                        Number = 11,
                        Position = new Position
                        {
                            X = new Length(160, LengthUnit.Millimeter),
                            Y = new Length(138, LengthUnit.Millimeter)
                        },
                        Quantity = Persistence.Positions.Quantity.Empty
                    };

                case 12:
                    return new ShotGlassPositionSetting
                    {
                        Number = 12,
                        Position = new Position
                        {
                            X = new Length(237, LengthUnit.Millimeter),
                            Y = new Length(139, LengthUnit.Millimeter)
                        },
                        Quantity = Persistence.Positions.Quantity.Empty
                    };

                default:
                    throw new ArgumentOutOfRangeException(nameof(positionNumber));
            }
        }
    }
}
