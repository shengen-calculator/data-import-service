using System.Collections.Generic;
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

        public EmailService(IAppConfigService appConfigService)
        {
            _appConfigService = appConfigService;
        }

        public List<SourceFile> GetSourceInfo()
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
        
    }
}