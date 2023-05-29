using API.Repositories.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly DataContext _context;

        public ServiceRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Hospital>> GetHospitalsAsync()
        {
            return await _context.Hospitals.ToListAsync();
        }

        public async Task<Service> GetServiceByIdAsync(int id)
        {
            return await _context.Services
            .Include(s=> s. ServiceType)
            .Include(s=>s.Hospital)
            .FirstOrDefaultAsync(s=>s.Id==id);
        }

        public async Task<IReadOnlyList<Service>> GetServicesAsync()
        {
            return await _context.Services
            .Include(s=> s. ServiceType)
            .Include(h=>h.Hospital)
            .ToListAsync();
        }

        public async Task<IReadOnlyList<ServiceType>> GetServiceTypesAsync()
        {
            return await _context.ServiceTypes.ToListAsync();
        }
    }
}
