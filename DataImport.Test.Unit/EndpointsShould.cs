using DataImport.Common.Enums;
using DataImport.Common.SharedModels;
using DataImport.Core.Services;
using DataImport.Infrastructure.Services;
using Moq;
using Xunit;

namespace DataImport.Test.Unit
{
    public class EndpointsShould
    {
        private readonly Mock<IAppConfigService> _mockConfigService;
        private readonly Mock<ILogService> _mockLogService;
        
        public EndpointsShould()
        {
            _mockConfigService = new Mock<IAppConfigService>();
            _mockLogService = new Mock<ILogService>();
        }
       
        [Fact]
        public async void GetVendor()
        {
            _mockConfigService.Setup(x => x.EndpointSettings)
                .Returns(() =>
                new EndpointSettings {
                    Key = "eyJhbGciOiJSUzI1Ni",
                    GetVendor = "https://europe"
                });
            
            var vendorService = new VendorService(_mockConfigService.Object, _mockLogService.Object);
            var vendors = await vendorService.GetVendors("admin@cde.com");
            Assert.NotNull(vendors);
            
        }
        
        [Fact]
        public async void PostLog()
        {
            _mockConfigService.Setup(x => x.EndpointSettings)
                .Returns(() =>
                    new EndpointSettings {
                        Key = "eyJhbGciOiJSUzI1Ni",
                        PostLog = "https://europe"
                    });
            
            var logService = new LogService(_mockConfigService.Object);
            await logService.Log(Level.Error, "test message", "SomeVendor");
            
        }
        
    }
}