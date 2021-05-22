using DataImport.Common.Attribute;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataImport.Worker
{
    public static class StartupDependencyInjection
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);

            services.Scan(scan => scan.FromExecutingAssembly()
                .FromApplicationDependencies(a => a.FullName.Contains("DataImport.Storage.Repositories")
                                                  || a.FullName.Contains("DataImport.Infrastructure")
                                                  || a.FullName.Contains("DataImport.Core"))
                .AddClasses(filter => filter.WithAttribute<ExposeForDIAttribute>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

        }
    }
}