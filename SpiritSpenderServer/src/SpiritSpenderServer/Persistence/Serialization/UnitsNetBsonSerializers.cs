using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using UnitsNet;

namespace SpiritSpenderServer.Persistence.Serialization
{
    public class LengthBsonSerializer : SerializerBase<Length>
    {
        public override Length Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
            => Length.Parse(context.Reader.ReadString());

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Length value)
            => context.Writer.WriteString(value.ToString());
    }

    public class SpeedBsonSerializer : SerializerBase<Speed>
    {
        public override Speed Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
            => Speed.Parse(context.Reader.ReadString());

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Speed value)
            => context.Writer.WriteString(value.ToString());
    }

    public class AccelerationBsonSerializer : SerializerBase<Acceleration>
    {
        public override Acceleration Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
            => Acceleration.Parse(context.Reader.ReadString());

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Acceleration value)
            => context.Writer.WriteString(value.ToString());
    }

    public class DurationBsonSerializer : SerializerBase<Duration>
    {
        public override Duration Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
            => Duration.Parse(context.Reader.ReadString());

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Duration value)
            => context.Writer.WriteString(value.ToString());
    }
}
