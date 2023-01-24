namespace SpiritSpenderServer.Persistence.SpiritDispenserSettings;

using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using UnitsNet;

public class SpiritDispenserSetting
{
    [BsonId]
    [JsonProperty]
    public string? Name { get; set; }

    [JsonProperty]
    public Duration DriveTimeFromBottleChangeToHomePos { get; set; }

    [JsonProperty]
    public Duration DriveTimeFromHomePosToBottleChange { get; set; }

    [JsonProperty]
    public Duration DriveTimeFromHomeToReleasePosition { get; set; }

    [JsonProperty]
    public Duration DriveTimeFromReleaseToHomePosition { get; set; }

    [JsonProperty]
    public Duration WaitTimeUntilSpiritIsReleased { get; set; }

    [JsonProperty]
    public Duration WaitTimeUntilSpiritIsRefilled { get; set; }
}
