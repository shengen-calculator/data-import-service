using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DataImport.Common.Attribute;
using DataImport.Common.Enums;
using DataImport.Core.Domain;
using DataImport.Infrastructure.Services;
using Newtonsoft.Json;

namespace DataImport.Core.Services
{
    [ExposeForDI]
    public class VendorService : IVendorService
    {
        private readonly IAppConfigService _appConfigService;
        private readonly ILogService _logService;

        public VendorService(IAppConfigService appConfigService, ILogService logService)
        {
            _appConfigService = appConfigService;
            _logService = logService;
        }

        public async Task<IEnumerable<Vendor>> GetVendors(string email)
        {
            
            var endpointSettings = _appConfigService.EndpointSettings;

            try
            {
                var url = $"{endpointSettings.GetVendor}?email={email}";
                var request = (HttpWebRequest) WebRequest.Create(url);
                request.Headers["Authorization"] = endpointSettings.Key;
                var response = (HttpWebResponse) request.GetResponse();
                if (response.StatusCode != HttpStatusCode.OK) return null;
                await using var responseStream = response.GetResponseStream();
                var serializer = new JsonSerializer();
                using var streamReader = new StreamReader(responseStream ?? throw new Exception("Empty reply from endpoint"), Encoding.UTF8);
                using var jsonReader = new JsonTextReader(streamReader);
                return serializer.Deserialize<IEnumerable<Vendor>>(jsonReader);
            }
            catch (Exception e)
            {
                await _logService.Log(Level.Error, $"Try to get vendors by '{email}'. {e.Message}", string.Empty);
                return null;
            }
        }

        public Task<Vendor> GetVendor(int vendorId)
        {
            throw new System.NotImplementedException();
        }
    }
}