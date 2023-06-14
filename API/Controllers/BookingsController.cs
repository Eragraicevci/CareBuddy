using Core.DTOs;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.BookingAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using API.Extenstions;
using Core.Dtos;

namespace API.Controllers
{
    public class BookingsController : BaseApiController
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;
        public BookingsController(IBookingService bookingService, IMapper mapper)
        {
            _mapper = mapper;
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<ActionResult<Booking>> CreateBooking(BookingDto bookingDto)
        {
            var username = HttpContext.User.GetUsername();

            var patientInfo = _mapper.Map<PatientInfoDto, PatientInfo>(bookingDto.Info);

            var booking = await _bookingService.CreateBookingAsync(username, bookingDto.AppointmentTypeId, bookingDto.AppointmentId, patientInfo);

            if (booking == null) return BadRequest(new ApiResponse(400, "Problem creating booking"));

            return Ok(booking);
        }

    }}