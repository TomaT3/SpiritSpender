namespace SpiritSpenderServer.API.SignalR;

using UnitsNet;

public interface IAxisHub
{
    Task PositionChanged(PositionDto position);
}


public class PositionDto
{
    public Length X { get; set; }
    public Length Y { get; set; }
}
