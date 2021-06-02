using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ioBroker.net;
using Microsoft.Extensions.Hosting;
using SpiritSpenderServer.Automatic;
using SpiritSpenderServer.Config;
using Exception = System.Exception;

namespace SpiritSpenderServer.HostedServices
{
    public class IoBrokerCommunicationService :IHostedService
    {
        private static string CURRENT_SHOT_COUNT_ID = "javascript.0.spiritspender.shots.count";
        private static string TOTAL_SHOT_COUNT_ID = "javascript.0.spiritspender.shots.totalcount";

        private readonly IIoBrokerDotNet _ioBroker;
        private readonly IoBrokerConfig _ioBrokerConfig;
        private readonly IAutomaticMode _automaticMode;


        public IoBrokerCommunicationService(IIoBrokerDotNet ioBroker, IoBrokerConfig ioBrokerConfig, IAutomaticMode automaticMode)
        {
            _ioBrokerConfig = ioBrokerConfig;
            _ioBroker = ioBroker;
            _automaticMode = automaticMode;

            _automaticMode.OneShotPoured += OneShotPouredHandler;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _ioBroker.ConnectAsync(TimeSpan.FromSeconds(_ioBrokerConfig.ConnectionTimeout));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void OneShotPouredHandler()
        {
            Task.Run(async () =>
            {
                await AddOneShotAsync(CURRENT_SHOT_COUNT_ID);
                await AddOneShotAsync(TOTAL_SHOT_COUNT_ID);
            });
        }

        private async Task AddOneShotAsync(string ioBrokerId)
        {
            var currentNumber = await _ioBroker.GetStateAsync<int>(ioBrokerId, TimeSpan.FromSeconds(5));
            currentNumber++;
            await _ioBroker.SetStateAsync(ioBrokerId, currentNumber);
        }
    }
}
