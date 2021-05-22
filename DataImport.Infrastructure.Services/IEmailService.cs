using System;
using System.Collections.Generic;
using System.Text;
using DataImport.Core.Domain;

namespace DataImport.Infrastructure.Services
{
    public interface IEmailService
    {
        void Run();
    }
}