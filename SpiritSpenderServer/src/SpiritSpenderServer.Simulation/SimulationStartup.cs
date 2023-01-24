namespace SpiritSpenderServer.Simulation;
using Microsoft.Extensions.DependencyInjection;
using SpiritSpenderServer.Interface.HardwareControl;
using SpiritSpenderServer.Simulation.HardwareControlMock;

public static class SimulationStartup
{
    public static void StartSimulation(IServiceCollection services)
    {
        services.AddSingleton<IGpioPinFactory, GpioPinFactoryMock>();
    }
}
