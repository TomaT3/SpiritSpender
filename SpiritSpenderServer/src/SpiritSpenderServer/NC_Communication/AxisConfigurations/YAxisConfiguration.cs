namespace SpiritSpenderServer.NC_Communication.AxisConfigurations;

using UnitsNet;
using UnitsNet.Units;

public class YAxisConfiguration : AxisConfiguration, IYAxisConfiguration
{
    public YAxisConfiguration() : base()
    {
        MaxSpeed = new Speed(80, SpeedUnit.MillimeterPerSecond);
        Acceleration = new Acceleration(80, AccelerationUnit.MillimeterPerSecondSquared);
    }

    public override string AxisName => "Y";
}