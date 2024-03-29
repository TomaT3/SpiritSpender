﻿namespace SpiritSpenderServer.HostedServices;

using ioBroker.net;
using Microsoft.Extensions.Options;
using SpiritSpenderServer.Automatic;
using SpiritSpenderServer.Config;
using Exception = System.Exception;

public class IoBrokerCommunicationService : IHostedService
{
    private static string CURRENT_SHOT_COUNT_ID = "javascript.0.spiritspender.shots.count";
    private static string TOTAL_SHOT_COUNT_ID = "javascript.0.spiritspender.shots.totalcount";

    private readonly IIoBrokerDotNet _ioBroker;
    private readonly IoBroker _ioBrokerConfig;
    private readonly IAutomaticMode _automaticMode;


    public IoBrokerCommunicationService(IIoBrokerDotNet ioBroker, IOptions<IoBroker> ioBrokerConfig, IAutomaticMode automaticMode)
    {
        _ioBrokerConfig = ioBrokerConfig.Value;
        _ioBroker = ioBroker;
        _automaticMode = automaticMode;

        if (_ioBrokerConfig.Enabled)
        {
            _automaticMode.OneShotPoured += OneShotPouredHandler;
        }
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        if (_ioBrokerConfig.Enabled)
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
        var result = await _ioBroker.TryGetStateAsync<int>(ioBrokerId, TimeSpan.FromSeconds(5));
        if (result.Success)
        {
            var newCurrentShotsCount = result.Value + 1;
            await _ioBroker.TrySetStateAsync(ioBrokerId, newCurrentShotsCount);
        }
    }
}
