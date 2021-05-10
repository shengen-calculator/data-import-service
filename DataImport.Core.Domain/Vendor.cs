using DataImport.Common.Enums;
using DataImport.Storage.Core;

namespace DataImport.Core.Domain
{
    public class Vendor  : IEntity<long>
    {
        public string ProviderId { get; set; }
        public long InternalId { get; set; }
        public int[] FieldOrder { get; set; }
        public string FileName { get; set; }
        public int HeaderRowCount { get; set; }
        public ImportType Type { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}