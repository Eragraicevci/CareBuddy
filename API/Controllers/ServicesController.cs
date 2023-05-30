using API.Errors;
using API.Helpers;
using API.Repositories.Interfaces;
using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
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
        public async Task<ActionResult<Pagination<ServiceDto>>> GetServices(
            [FromQuery] ServiceSpecParams serviceParams)
        {
            var spec = new ServiceTypeAndHospitalsSpecification(serviceParams);
            var countSpec = new ServicesWithFiltersForCountSpecification(serviceParams);

            var totalItems = await _servicesRepo.CountAsync(countSpec);
            var services = await _servicesRepo.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<ServiceDto>>(services);

            return Ok(new Pagination<ServiceDto>(serviceParams.PageIndex,
                serviceParams.PageSize, totalItems, data));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceDto>> GetService(int id)
        {
            var spec = new ServiceTypeAndHospitalsSpecification(id);

            var service = await _servicesRepo.GetEntityWithSpec(spec);

            if (service == null)
                return NotFound(new ApiResponse(404));

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
