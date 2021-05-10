using System.IO;
using DataImport.Common.Attribute;
using DataImport.Common.SharedModels;
using DataImport.Infrastructure.Services;
using Microsoft.Extensions.Configuration;

namespace DataImport.Core.Services
{
    [ExposeForDI]
    public class AppConfigService : IAppConfigService
    {
        #region Ctor

        public AppConfigService()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            var root = configurationBuilder.Build();

            EndpointSettings = root.GetSection("Endpoints").Get<EndpointSettings>();
            EmailSettings = root.GetSection("Email").Get<EmailSettings>();
            ConnectionStringSettings = root.GetSection("ConnectionStrings").Get<ConnectionStringSettings>();
            
        }

        #endregion
        public ConnectionStringSettings ConnectionStringSettings { get; }
        public EndpointSettings EndpointSettings { get; }
        public EmailSettings EmailSettings { get; }
    }
}