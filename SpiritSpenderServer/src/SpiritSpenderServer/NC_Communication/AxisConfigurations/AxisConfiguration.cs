namespace SpiritSpenderServer.NC_Communication.AxisConfigurations;

using UnitsNet;

public abstract class AxisConfiguration : IAxisConfiguration
{
    public abstract string AxisName { get; }
    //private const string a = "steps_per_mm";
    //private const string a = "max_rate_mm_per_min";
    //private const string a = "acceleration_mm_per_sec2";
    //private const string a = "max_travel_mm";
    //private const string a = "soft_limits";

    public Acceleration Acceleration { get; set; }
    public Speed MaxSpeed { get; set; }

    public AxisConfiguration()
    {
    }

    void GetSettings()
    {
    }
}