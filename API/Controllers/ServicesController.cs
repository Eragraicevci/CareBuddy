using API.Repositories.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : BaseApiController
    {
        
        private readonly IGenericRepository<Service> _servicesRepo;
       private readonly IGenericRepository<Hospital> _hospitalRepo;
        private readonly IGenericRepository<ServiceType> _serviceTypeRepo;
        

        public ServicesController(IGenericRepository<Service> servicesRepo,
            IGenericRepository<ServiceType> serviceTypeRepo, 
            IGenericRepository<Hospital> hospitalRepo)
        {
            _servicesRepo = servicesRepo;
            _serviceTypeRepo = serviceTypeRepo;
            _hospitalRepo = hospitalRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Service>>> GetServices()
        {
            var services = await _servicesRepo.ListAllAsync();

            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(int id)
        {
            return await _servicesRepo.GetByIdAsync(id);
        }

           [HttpGet("hospitals")]
        public async Task<ActionResult<IReadOnlyList<Hospital>>> GetHospitals()
        {
            return Ok(await _hospitalRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ServiceType>>> GetServiceTypes()
        {
            return Ok(await _serviceTypeRepo.ListAllAsync());
        }

    }
}
