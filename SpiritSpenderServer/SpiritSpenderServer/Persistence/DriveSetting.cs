using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitsNet;

namespace SpiritSpenderServer.Persistence
{
    public class DriveSetting
    {
        [JsonIgnore]
        [BsonId]
        public ObjectId InternalId { get; set; }
        
        [JsonProperty]
        public string DriveName { get; set; }
        [JsonProperty]
        public int StepsPerRevolution { get; set; }
        [JsonProperty]
        public Length SpindelPitch { get; set; }
       [JsonProperty]
        public Speed MaxSpeed { get; set; }
        [JsonProperty]
        public Acceleration Acceleration { get; set; }
        [JsonProperty]
        public int EnableGpioPin { get; set; }
        [JsonProperty]
        public int DirectionGpioPin { get; set; }
        [JsonProperty]
        public int StepGpioPin { get; set; }
        [JsonProperty]
        public bool ReverseDirection { get; set; }

    }
}
