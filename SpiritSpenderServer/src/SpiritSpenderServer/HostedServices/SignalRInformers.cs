using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SpiritSpenderServer.API.SignalR.Hubs;

namespace SpiritSpenderServer.HostedServices
{
    public class SignalRInformers : IHostedService
    {
        private readonly AxisHubInformer _axisHubInformer;

        public SignalRInformers(AxisHubInformer axisHubInformer)
        {
            _axisHubInformer = axisHubInformer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
