using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using UnitsNet;

namespace SpiritSpenderServer.Persistence.Positions
{
    public class ShotGlassPositionSetting
    {
        [BsonId]
        [JsonProperty]
        public int Number { get; set; }
        [JsonProperty]
        public Position Position { get; set; } = null!;
        [JsonProperty]
        public Quantity Quantity { get; set; }
    }

    public class Position 
    {
        [JsonProperty]
        public Length X { get; set; }
        [JsonProperty]
        public Length Y { get; set; }
    }

    public enum Quantity
    {
        Empty = 0,
        OneShot = 1,
        DoubleShot = 2
    }
}
