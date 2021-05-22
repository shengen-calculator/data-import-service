using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataImport.Common.Attribute;
using DataImport.Core.Domain;
using DataImport.Infrastructure.Services;
using System.IO.Compression;

namespace DataImport.Core.Services
{
    [ExposeForDI]
    public class DataImportService : IDataImportService
    {
        private readonly IAppConfigService _appConfigService;
        private readonly ILogService _logService;

        public DataImportService(IAppConfigService appConfigService, ILogService logService)
        {
            _appConfigService = appConfigService;
            _logService = logService;
        }

        public Task Import(Vendor vendor, byte[] bytes, string fileName)
        {
            var ext = Path.GetExtension(fileName);

            //decompression
            if (ext == ".zip")
            {
                bytes = Unzip(bytes, ref ext);
            }
            
            return Task.CompletedTask;
        }



        private static byte[] Unzip(byte[] zippedBuffer, ref string ext)
        {
            using var zippedStream = new MemoryStream(zippedBuffer);
            using var archive = new ZipArchive(zippedStream);
            var entry = archive.Entries.FirstOrDefault();
            if (entry == null) return null;
            ext = Path.GetExtension(entry.Name);
            using var unzippedEntryStream = entry.Open();
            using var ms = new MemoryStream();
            unzippedEntryStream.CopyTo(ms);
            return ms.ToArray();
        }

        public Task StartHandler(Vendor vendor)
        {
            throw new System.NotImplementedException();
        }
    }
}