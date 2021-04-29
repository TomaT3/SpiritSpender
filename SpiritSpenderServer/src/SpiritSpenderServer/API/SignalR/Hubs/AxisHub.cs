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
        public AxisHub(IXAxis xAxis, IYAxis yAxis)
        {
            xAxis.PositionChanged += PositionChangedHandler;
            yAxis.PositionChanged += PositionChangedHandler;
        }

        private async void PositionChangedHandler(string axisName, Length currentPosition)
        {
            await Clients.All.PositionChanged(new PositionDto() { AxisName = axisName, Positon = currentPosition });
        }
    }
}
