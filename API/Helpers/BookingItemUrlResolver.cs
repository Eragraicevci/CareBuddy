using AutoMapper;
using Core.DTOs;
using Core.Entities.BookingAggregate;

namespace API.Helpers
{
    public class BookingItemUrlResolver : IValueResolver<BookingItem, BookingItemDto, string>
    {
        private readonly IConfiguration _config;

        public BookingItemUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(
            BookingItem source,
            BookingItemDto destination,
            string destMember,
            ResolutionContext context
        )
        {
            if (!string.IsNullOrEmpty(source.ItemBooked.PictureUrl))
            {
                return _config["ApiUrl"] + source.ItemBooked.PictureUrl;
            }

            return null;
        }
    }
}
