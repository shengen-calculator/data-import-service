using System.Collections.Generic;
using System.Threading.Tasks;
using DataImport.Core.Domain;

namespace DataImport.Infrastructure.Services
{
    public interface IVendorService
    {
        Task<IEnumerable<Vendor>> GetVendors(string email);
        Task<Vendor> GetVendor(int vendorId);
    }
}