using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpiritSpenderServer.Persistence
{
    interface ISpiritSpenderDBContext
    {
        IMongoCollection<DriveSetting> DriveSettings { get; }
    }
}
