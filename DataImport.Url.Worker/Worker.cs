using System;
using System.Threading;
using System.Threading.Tasks;
using DataImport.Infrastructure.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DataImport.Url.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IAppConfigService _appConfigService;

        public Worker(ILogger<Worker> logger, IAppConfigService appConfigService)
        {
            _logger = logger;
            _appConfigService = appConfigService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var vendorEndpoint = _appConfigService.EndpointSettings.GetVendor;
            
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}