using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using DataImport.Common.Attribute;
using DataImport.Core.Domain;
using DataImport.Infrastructure.Services;
using OpenPop.Pop3;

namespace DataImport.Core.Services
{
    [ExposeForDI]
    public class EmailService : IEmailService
    {
        private readonly IAppConfigService _appConfigService;
        private readonly IVendorService _vendorService;

        public EmailService(IAppConfigService appConfigService, IVendorService vendorService)
        {
            _appConfigService = appConfigService;
            _vendorService = vendorService;
        }

        
        public void Run()
        {
            var sourceInfo = GetSourceInfo();
            if (sourceInfo == null) return;
            foreach (var info in sourceInfo)
            {
                var vendor = _vendorService.GetVendor(info.Sender, info.FileName);
                if (vendor == null) continue;
                var ext = Path.GetExtension(info.FileName);
                    
                //decompression
                if (ext == ".zip")
                {
                    info.Bytes = Unzip(info.Bytes, ref ext);
                }
                
                // save to fs
                File.WriteAllBytes($"{_appConfigService.LocalFolderSettings.In}{vendor.ProviderId}{ext}", info.Bytes); 

            }

        }
        private List<SourceFile> GetSourceInfo()
        {
            var emailSetting = _appConfigService.EmailSettings;
            
            using (var client = new Pop3Client())
            {
                client.Connect(emailSetting.Hostname, emailSetting.Port, emailSetting.UseSsl);

                client.Authenticate(emailSetting.Username, emailSetting.Password);

                var ids = client.GetMessageUids();
                
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);  

                for (var i = 0; i < ids.Count; i++)
                {
                    var currentUidOnServer = ids[i];
                    
                    var message = client.GetMessage(i + 1);

                    foreach (var result in message.FindAllAttachments().Select(attachment => new List<SourceFile>
                    {
                        new SourceFile
                        {
                            EmailId = currentUidOnServer,
                            FileName = attachment.FileName,
                            Bytes = attachment.Body,
                            Sender = message.Headers.From.Address
                        }
                    }).Where(result => result.Any()))
                    {
                        return result;
                    }
                }
            }
            return null;
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

    }
}