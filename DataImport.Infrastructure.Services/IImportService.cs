using System.Threading.Tasks;

namespace DataImport.Infrastructure.Services
{
    public interface IImportService
    {
        Task Run();
    }
}