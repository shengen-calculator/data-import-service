using System.Threading.Tasks;
using DataImport.Core.Domain;

namespace DataImport.Infrastructure.Services
{
    public interface IDataImportService
    {
        Task Import(Vendor vendor, byte[] bytes, string fileName);

        Task StartHandler(Vendor vendor);
    }
}