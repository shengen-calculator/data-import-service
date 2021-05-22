using DataImport.Common.SharedModels;

namespace DataImport.Infrastructure.Services
{
    public interface IAppConfigService
    {
        EndpointSettings EndpointSettings { get; }
        EmailSettings EmailSettings { get; }
    }
}