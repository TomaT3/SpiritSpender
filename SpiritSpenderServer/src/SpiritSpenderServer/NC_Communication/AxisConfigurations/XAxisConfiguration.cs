namespace SpiritSpenderServer.NC_Communication.AxisConfigurations;

using UnitsNet;
using UnitsNet.Units;

public class XAxisConfiguration : AxisConfiguration, IXAxisConfiguration
{
    public XAxisConfiguration() : base()
    {
        MaxSpeed = new Speed(50, SpeedUnit.MillimeterPerSecond);
        Acceleration = new Acceleration(40, AccelerationUnit.MillimeterPerSecondSquared);
    }

    public override string AxisName => "X";
}