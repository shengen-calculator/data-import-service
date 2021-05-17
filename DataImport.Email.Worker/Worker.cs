using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataImport.Common.Enums;
using DataImport.Infrastructure.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DataImport.Email.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ILogService _logService;
        private readonly IEmailService _emailService;
        private readonly IVendorService _vendorService;
        private readonly IDataImportService _dataImportService;

        public Worker(ILogger<Worker> logger, IEmailService emailService, IVendorService vendorService, IDataImportService dataImportService, ILogService logService)
        {
            _logger = logger;
            _emailService = emailService;
            _vendorService = vendorService;
            _dataImportService = dataImportService;
            _logService = logService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _logService.Log(Level.Info, "DataImport service (EMAIL) started!", String.Empty);
            while (!stoppingToken.IsCancellationRequested)
            {

                var sourceInfo = _emailService.GetSourceInfo();

                if (sourceInfo != null)
                {
                    
                    foreach (var info in sourceInfo)
                    {
                        var vendors = await _vendorService.GetVendors(info.Sender);
                        var vendor = vendors.SingleOrDefault(x => 
                            info.FileName.StartsWith(x.FileName) && 
                            x.IsActive && 
                            x.Type == ImportType.Email);
                        if (vendor == null) continue;
                        await _dataImportService.Import(vendor, info.Bytes, info.FileName);
                        await _dataImportService.StartHandler(vendor);
                    }

                }
                
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                await Task.Delay(1 * 60 * 1000, stoppingToken);
            }
        }
    }
}