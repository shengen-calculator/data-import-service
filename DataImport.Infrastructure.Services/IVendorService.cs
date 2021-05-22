using System.Threading.Tasks;
using DataImport.Core.Domain;

namespace DataImport.Infrastructure.Services
{
    public interface IVendorService
    {
        Task InitCollection();
        Vendor GetVendor(string email, string fileName);
    }
}