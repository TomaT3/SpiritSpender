﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using SpiritSpenderServer.Persistence.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitsNet;

namespace SpiritSpenderServer.Persistence.DriveSettings
{
    public class DriveSetting
    {
        [BsonId]
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
        public bool ReverseDirection { get; set; }

    }
}
