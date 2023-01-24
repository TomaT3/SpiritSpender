namespace SpiritSpenderServer.Persistence.StatusLampSettings;

using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using UnitsNet;

public class StatusLampSetting
{
    [BsonId]
    [JsonProperty]
    public string? Name { get; set; }

    [JsonProperty]
    public Duration BlinkTimeOn { get; set; }
    [JsonProperty]
    public Duration BlinkTimeOff { get; set; }
}
