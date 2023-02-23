namespace SpiritSpenderServer.API.SignalR.Hubs;

using Microsoft.AspNetCore.SignalR;
using NC_Communication;
using Persistence.Positions;
using UnitsNet.Units;

public class AxisHubInformer
{
    private readonly IHubContext<AxisHub, IAxisHub> _hubContext;

    public AxisHubInformer(INcCommunication ncCommunication, IHubContext<AxisHub, IAxisHub> hubContext)
    {
        _hubContext = hubContext;
        ncCommunication.PositionChanged += PositionChangedHandler;
    }

    private async void PositionChangedHandler(Position currentPosition)
    {
        var positionDto = new PositionDto() { X = currentPosition.X.ToUnit(LengthUnit.Millimeter), Y = currentPosition.Y.ToUnit(LengthUnit.Millimeter) };

        await _hubContext.Clients.All.PositionChanged(positionDto);
    }
}
