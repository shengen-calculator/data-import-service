using System.Threading.Tasks;
using DataImport.Common.Enums;

namespace DataImport.Infrastructure.Services
{
    public interface ILogService
    {
        Task Log(Level level, string message, string vendorName);
    }
}