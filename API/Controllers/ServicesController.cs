using API.Repositories.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : BaseApiController
    {
        private IServiceRepository _repo;

        public ServicesController(IServiceRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Service>>> GetProducts()
        {
            var services = await _repo.GetServicesAsync();

            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetProduct(int id)
        {
            return await _repo.GetServiceByIdAsync(id);
        }

    }
}
