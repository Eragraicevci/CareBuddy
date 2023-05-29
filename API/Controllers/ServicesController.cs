using API.Repositories.Interfaces;
using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Specifications;
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
        private readonly IMapper _mapper;

        public ServicesController(
            IGenericRepository<Service> servicesRepo,
            IGenericRepository<ServiceType> serviceTypeRepo,
            IGenericRepository<Hospital> hospitalRepo,
            IMapper mapper
        )
        {
            _mapper = mapper;
            _servicesRepo = servicesRepo;
            _serviceTypeRepo = serviceTypeRepo;
            _hospitalRepo = hospitalRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ServiceDto>>> GetServices()
        {
            var spec = new ServiceTypeAndHospitalsSpecification();

            var services = await _servicesRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<ServiceDto>>(services));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDto>> GetService(int id)
        {
            var spec = new ServiceTypeAndHospitalsSpecification(id);

            var service =  await _servicesRepo.GetEntityWithSpec(spec);

            return _mapper.Map<Service, ServiceDto>(service);

             
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
