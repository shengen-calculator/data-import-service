using System.Diagnostics;
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
            var isService = !(Debugger.IsAttached);
            if (isService)
            {
                var processModule = Process.GetCurrentProcess().MainModule;
                if (processModule != null)
                {
                    var pathToExe = processModule.FileName;
                    var pathToContentRoot = Path.GetDirectoryName(pathToExe);
                    Directory.SetCurrentDirectory(pathToContentRoot);
                }
            }
            
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            var root = configurationBuilder.Build();

            EndpointSettings = root.GetSection("Endpoints").Get<EndpointSettings>();
            EmailSettings = root.GetSection("Email").Get<EmailSettings>();
            LocalFolderSettings = root.GetSection("LocalFolder").Get<LocalFolderSettings>();
            
        }

        #endregion
        public EndpointSettings EndpointSettings { get; }
        public EmailSettings EmailSettings { get; }
        public LocalFolderSettings LocalFolderSettings { get; }
    }
}