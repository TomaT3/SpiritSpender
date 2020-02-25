using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitsNet;

namespace SpiritSpenderServer.Persistence.SpiritDispenserSettings
{
    public class SpiritDispenserSetting
    {
        [BsonId]
        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public Duration DriveTimeToReleaseTheSpirit { get; set; }

        [JsonProperty]
        public Duration DriveTimeToCloseTheSpiritSpender { get; set; }

        [JsonProperty]
        public Duration WaitTimeUntilSpiritIsReleased { get; set; }
    }
}
