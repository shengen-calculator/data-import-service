using System;
using System.Threading;
using System.Threading.Tasks;
using DataImport.Common.Enums;
using DataImport.Infrastructure.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DataImport.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ILogService _logService;
        private readonly IVendorService _vendorService;
        private readonly IEmailService _emailService;

        public Worker(ILogger<Worker> logger, ILogService logService, IEmailService emailService, IVendorService vendorService)
        {
            _logger = logger;
            _logService = logService;
            _emailService = emailService;
            _vendorService = vendorService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _logService.Log(Level.Info, "DataImport service started!", string.Empty);
            await _vendorService.InitCollection();
            
            while (!stoppingToken.IsCancellationRequested)
            {
                var emailTask = new Task( () => _emailService.Run());
                emailTask.Start();
                
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1 * 60 * 1000, stoppingToken);
            }
        }
    }
}