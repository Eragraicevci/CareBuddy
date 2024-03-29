using Core.Entities;

namespace API.Repositories.Interfaces
{
    public interface IServiceRepository
    {
        Task<Service> GetServiceByIdAsync(int id);
        Task<IReadOnlyList<Service>> GetServicesAsync();

         Task<IReadOnlyList<Hospital>> GetHospitalsAsync();

          Task<IReadOnlyList<ServiceType>> GetServiceTypesAsync();

    }
}