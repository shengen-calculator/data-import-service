using System.Threading.Tasks;
using DataImport.Common.Attribute;
using DataImport.Core.Domain;
using DataImport.Infrastructure.Services;

namespace DataImport.Core.Services
{
    [ExposeForDI]
    public class UrlService : IUrlService
    {
        public Task<SourceFile> GetSourceByUrl()
        {
            throw new System.NotImplementedException();
        }
    }
}