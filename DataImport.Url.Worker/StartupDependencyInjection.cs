using DataImport.Common.Attribute;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataImport.Url.Worker
{
    public static class StartupDependencyInjection
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);

            services.Scan(scan => scan.FromExecutingAssembly()
                .FromApplicationDependencies(a => a.FullName.Contains("DataImport.Infrastructure.Services")
                                                  || a.FullName.Contains("DataImport.Core.Services"))
                .AddClasses(filter => filter.WithAttribute<ExposeForDIAttribute>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

        }
    }
}