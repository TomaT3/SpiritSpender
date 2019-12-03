using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpiritSpenderServer.Persistence
{
    public class DriveSetting
    {
        [BsonId]
        public ObjectId InternalId { get; set; }
        public string DriveName { get; set; }
        public int MaxSpeed { get; set; }
        public double Acceleration { get; set; }
    }
}
