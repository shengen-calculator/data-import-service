using DataImport.Common.Enums;
using DataImport.Storage.Core;

namespace DataImport.Core.Domain
{
    public class Vendor  : IEntity<long>
    {
        public string ProviderId { get; set; }
        public long InternalId { get; set; }
        public string Email { get; set; }
        public int[] DownloadDays { get; set; }
        public string DownloadTime { get; set; }
        public string FileName { get; set; }
        public ImportType Type { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}