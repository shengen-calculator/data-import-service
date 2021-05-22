using System.Threading.Tasks;

namespace DataImport.Infrastructure.Services
{
    public interface IHandlerService
    {
        Task Run();
    }
}