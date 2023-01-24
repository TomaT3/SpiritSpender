namespace SpiritSpenderServer.Persistence.DriveSettings;

using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using UnitsNet;

public class DriveSetting
{
    [BsonId]
    [JsonProperty]
    public string? DriveName { get; set; }
    [JsonProperty]
    public int StepsPerRevolution { get; set; }
    [JsonProperty]
    public Length SpindlePitch { get; set; }
    [JsonProperty]
    public Speed MaxSpeed { get; set; }
    [JsonProperty]
    public Acceleration Acceleration { get; set; }
    [JsonProperty]
    public Length SoftwareLimitMinus { get; set; }
    [JsonProperty]
    public Length SoftwareLimitPlus { get; set; }
    [JsonProperty]
    public bool ReverseDirection { get; set; }
    [JsonProperty]
    public Length ReferencePosition { get; set; }
    [JsonProperty]
    public DrivingDirection ReferenceDrivingDirection { get; set; }
    [JsonProperty]
    public Speed ReferenceDrivingSpeed { get; set; }
}

public enum DrivingDirection
{
    Positive = 0,
    Negative = 1
}
