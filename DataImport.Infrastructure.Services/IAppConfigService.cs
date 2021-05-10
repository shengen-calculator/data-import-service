using DataImport.Common.SharedModels;

namespace DataImport.Infrastructure.Services
{
    public interface IAppConfigService
    {
        ConnectionStringSettings ConnectionStringSettings { get; }
        EndpointSettings EndpointSettings { get; }
        EmailSettings EmailSettings { get; }
    }
}