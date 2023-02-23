namespace SpiritSpenderServer.NC_Communication.AxisConfigurations;

using UnitsNet;

public interface IAxisConfiguration
{
    string AxisName { get; }
    Acceleration Acceleration { get; set; }
    Speed MaxSpeed { get; set; }
}