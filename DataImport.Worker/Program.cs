using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DataImport.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new DefaultServiceProviderFactory())
                .ConfigureServices((hostContext, services) =>
                {
                    services.ConfigureDependencyInjection(hostContext.Configuration);
                    services.AddHostedService<Worker>(); 
                    
                });
    }
}