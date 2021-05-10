namespace DataImport.Core.Domain
{
    public class SourceFile
    {
        public string Sender { get; set; }
        public string FileName { get; set; }
        public string EmailId { get; set; }
        
        public Vendor Vendor { get; set; }
        
        public byte[] Bytes { get; set; }
    }
}