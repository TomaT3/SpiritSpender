using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpiritSpenderServer.API.SignalR.Hubs
{
    using HardwareControl.Axis;
    using Microsoft.AspNetCore.SignalR;
    using UnitsNet;
    using UnitsNet.Units;

    public class AxisHub : Hub<IAxisHub>
    {
        
    }
}
