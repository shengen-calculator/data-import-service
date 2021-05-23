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

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new DefaultServiceProviderFactory())
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.ConfigureDependencyInjection(hostContext.Configuration);
                    services.AddHostedService<Worker>(); 
                    
                });
    }
}