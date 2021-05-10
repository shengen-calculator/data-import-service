namespace DataImport.Common.SharedModels
{
    public class EmailSettings
    {
        public string Hostname { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}