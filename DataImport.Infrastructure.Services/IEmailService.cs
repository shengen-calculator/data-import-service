using System.Collections.Generic;
using DataImport.Core.Domain;

namespace DataImport.Infrastructure.Services
{
    public interface IEmailService
    {
        List<SourceFile> GetSourceInfo();
    }
}