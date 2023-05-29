using AutoMapper;
using Core.DTOs;
using Core.Entities;

namespace API.Helpers
{
    public class ServiceUrlResolver : IValueResolver<Service, ServiceDto, string>
    {
        private readonly IConfiguration _config;

        public ServiceUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(
            Service source,
            ServiceDto destination,
            string destMember,
            ResolutionContext context
        )
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl;
            }
            return null;
        }
    }
}
