using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DataImport.Common.Attribute;
using DataImport.Common.Enums;
using DataImport.Infrastructure.Services;

namespace DataImport.Core.Services
{
    [ExposeForDI]
    public class LogService : ILogService

    {
        private readonly IAppConfigService _appConfigService;

        public LogService(IAppConfigService appConfigService)
        {
            _appConfigService = appConfigService;
        }

        public async Task Log(Level level, string message, string vendorName)
        {
            var endpointSettings = _appConfigService.EndpointSettings;

            var url = $"{endpointSettings.PostLog}";
            var request = (HttpWebRequest) WebRequest.Create(url);
            request.Headers["Authorization"] = endpointSettings.Key;
            
            var postData = $"level={level}";
            postData += "&vendor=" + Uri.EscapeDataString(vendorName);
            postData += "&message=" + Uri.EscapeDataString(message);
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            await using var stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);
            await request.GetResponseAsync();
        }
    }
}