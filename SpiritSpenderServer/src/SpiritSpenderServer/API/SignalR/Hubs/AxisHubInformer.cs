using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SpiritSpenderServer.HardwareControl.Axis;
using UnitsNet;
using UnitsNet.Units;

namespace SpiritSpenderServer.API.SignalR.Hubs
{
    public class AxisHubInformer
    {
        private readonly IHubContext<AxisHub, IAxisHub> _hubContext;

        public AxisHubInformer(IXAxis xAxis, IYAxis yAxis, IHubContext<AxisHub, IAxisHub> hubContext)
        {
            _hubContext = hubContext;
            xAxis.PositionChanged += PositionChangedHandler;
            yAxis.PositionChanged += PositionChangedHandler;
        }

        private async void PositionChangedHandler(string axisName, Length currentPosition)
        {
            var positionDto = new PositionDto() { AxisName = axisName, Positon = currentPosition.ToUnit(LengthUnit.Millimeter) };
            
            await _hubContext.Clients.All.PositionChanged(positionDto);
        }
    }
}
