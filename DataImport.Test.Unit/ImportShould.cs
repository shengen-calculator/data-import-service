using System.IO;
using DataImport.Common.SharedModels;
using DataImport.Core.Domain;
using DataImport.Core.Services;
using DataImport.Infrastructure.Services;
using Moq;
using Xunit;

namespace DataImport.Test.Unit
{
    public class ImportShould
    {
        private readonly Mock<IAppConfigService> _mockConfigService;
        private readonly Mock<ILogService> _mockLogService;

        public ImportShould()
        {
            _mockLogService = new Mock<ILogService>();
            _mockConfigService = new Mock<IAppConfigService>();;
        }

        [Fact]
        public async void Import()
        {
            _mockConfigService.Setup(x => x.ConnectionStringSettings)
                .Returns(() =>
                    new ConnectionStringSettings() {
                        DefaultConnection = "Server=.;Initial Catalog=di;User Id=sa;Password=pa$$word#"
                    });
            
            var dataImportService = new DataImportService(_mockConfigService.Object, _mockLogService.Object);

            await dataImportService.Import(new Vendor
            {
                //FieldOrder = new[] {2,1,3,4,5},
                FieldOrder = new[] {4, 2, 3, 6, 1},
                InternalId = 25,
                HeaderRowCount = 1, 
                FileName = "ICars_stan_i (38).zip",
                ProviderId = "5670706842959872",
                CodePage = 1251,
                BranchOrder = new[] {34, 42, 7, 13, 30, 47}
            }, File.ReadAllBytes("ICars_stan_i (38).zip"));

        }
        
    }
}