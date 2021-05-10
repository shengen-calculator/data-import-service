using System.Threading.Tasks;
using DataImport.Core.Domain;

namespace DataImport.Infrastructure.Services
{
    public interface IUrlService
    {
        Task<SourceFile> GetSourceByUrl();
    }
}