namespace SpiritSpenderServer.Persistence.Serialization;

using MongoDB.Bson.Serialization;
using UnitsNet;

public class UnitNetSerializationProvider : IBsonSerializationProvider
{
    public IBsonSerializer? GetSerializer(Type type)
        => type switch
        {
            Type _ when type == typeof(Length) => new LengthBsonSerializer(),
            Type _ when type == typeof(Speed) => new SpeedBsonSerializer(),
            Type _ when type == typeof(Acceleration) => new AccelerationBsonSerializer(),
            Type _ when type == typeof(Duration) => new DurationBsonSerializer(),
            _ => null,
        };
}
