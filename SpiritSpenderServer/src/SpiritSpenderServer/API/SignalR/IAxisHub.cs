namespace SpiritSpenderServer.API.SignalR;

using UnitsNet;

public interface IAxisHub
{
    Task PositionChanged(PositionDto position);
}


public class PositionDto
{
    public string? AxisName { get; set; }
    public Length Positon { get; set; }
}
