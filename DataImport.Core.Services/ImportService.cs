using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DataImport.Common.Attribute;
using DataImport.Infrastructure.Services;

namespace DataImport.Core.Services
{
    [ExposeForDI]
    public class ImportService : IImportService
    {
        private readonly IAppConfigService _appConfigService;
        private readonly IVendorService _vendorService;

        public ImportService(IAppConfigService appConfigService, IVendorService vendorService)
        {
            _appConfigService = appConfigService;
            _vendorService = vendorService;
        }

        public async Task Run()
        {
            var directoryInfo = new DirectoryInfo(_appConfigService.LocalFolderSettings.In);
            var files = directoryInfo.GetFiles();

            foreach (var file in files)
            {
                var ext = Path.GetExtension(file.Name);

                if (ext != ".csv" && ext != ".xls" && ext != ".xlsx") continue;
                var fileName = Path.GetFileNameWithoutExtension(file.Name);
                File.Move($"{file.FullName}", 
                    $"{_appConfigService.LocalFolderSettings.Temp}{file.Name}", true);

                var vendor = _vendorService.GetVendorById(fileName);
                
                //send request for handling
                
                var url = $"{_appConfigService.EndpointSettings.PostVendor}";
                var request = (HttpWebRequest) WebRequest.Create(url);
                request.Headers["Authorization"] = _appConfigService.EndpointSettings.Key;
            
                var postData = $"name={vendor.Name}";
                postData += "&providerId=" + Uri.EscapeDataString(vendor.ProviderId);
                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                await using var stream = request.GetRequestStream();
                stream.Write(data, 0, data.Length);
                var response = await request.GetResponseAsync();
                var webResponse = (HttpWebResponse)response;
                if (webResponse.StatusCode != HttpStatusCode.OK)
                {
                    File.Move($"{_appConfigService.LocalFolderSettings.Temp}{file.Name}", 
                        $"{_appConfigService.LocalFolderSettings.Error}{file.Name}");
                }
            }
        }
    }
}