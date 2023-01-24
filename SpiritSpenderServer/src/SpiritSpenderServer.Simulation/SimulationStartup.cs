using Microsoft.Extensions.DependencyInjection;
using SpiritSpenderServer.Interface.HardwareControl;
using SpiritSpenderServer.Simulation.HardwareControlMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritSpenderServer.Simulation;

public static class SimulationStartup
{
    public static void StartSimulation(IServiceCollection services)
    {
        services.AddSingleton<IGpioPinFactory, GpioPinFactoryMock>();
    }
}
