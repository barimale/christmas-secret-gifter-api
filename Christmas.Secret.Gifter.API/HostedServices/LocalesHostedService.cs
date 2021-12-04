using Albergue.Administrator.HostedServices.Hub;
using Albergue.Administrator.Model;
using Albergue.Administrator.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Albergue.Administrator.HostedServices
{
    public class LocalesHostedService : IHostedService
    {
        private readonly ILocalesGenerator _localesGenerator;
        private readonly IImageExtractor _extractor;
        private readonly ILogger<LocalesHostedService> _logger;
        private readonly PubSub.Hub _hub;
        private readonly IHubContext<LocalesStatusHub, ILocalesStatusHub> _broadcastLocalesStatus;

        public LocalesHostedService()
        {
            _hub = PubSub.Hub.Default;
        }

        public LocalesHostedService(
            ILogger<LocalesHostedService> logger,
            IServiceProvider serviceProvider,
            IHubContext<LocalesStatusHub, ILocalesStatusHub> broadcastLocalesStatus)
            : this()
        {
            _broadcastLocalesStatus = broadcastLocalesStatus;
            _logger = logger;
            _extractor = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IImageExtractor>();
            _localesGenerator = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ILocalesGenerator>();
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Locales Hosted Service running.");

            _hub.Subscribe<ShopItem>(async (item) =>
            {
                await DoWorkAsync();
            });

            _hub.Subscribe<Category>(async (item) =>
            {
                await DoWorkAsync();
            });

            _hub.Subscribe<Language>(async (item) =>
            {
                await DoWorkAsync();
            });

            _hub.Subscribe<string>(async (item) =>
            {
                await DoWorkAsync();
            });

            return Task.CompletedTask;
        }

        private async Task DoWorkAsync()
        {
            var id = Guid.NewGuid().ToString();

            try
            {
                _logger.LogInformation(
                    "Locales creation in progress. ");

                await _broadcastLocalesStatus.Clients.All.OnStartAsync(id);

                await Task.Delay(3000);

                await Task.WhenAll(
                    Task.Run(async () =>
                    {
                        await _localesGenerator.GenerateAsync();
                    }),
                    Task.Run(async () =>
                    {
                        await _extractor.SaveLocallyAsync();
                    })
                );


                _logger.LogInformation(
                    "Locales creation is finished. ");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            finally
            {
                await _broadcastLocalesStatus.Clients.All.OnFinishAsync(id);
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Locales Hosted Service is stopping.");

            return Task.CompletedTask;
        }
    }
}
