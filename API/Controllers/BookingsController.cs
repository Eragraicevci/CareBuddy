using Core.DTOs;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.BookingAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using API.Extenstions;
using Core.Dtos;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    // [Authorize]
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

            var booking = await _bookingService.CreateBookingAsync(username, bookingDto.AppointmetTypeId, bookingDto.AppointmentId, patientInfo);

            if (booking == null) return BadRequest(new ApiResponse(400, "Problem creating booking"));

            return Ok(booking);
        }

         [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Booking>>> GetBookingsForPatient()
        {
            var patientName = HttpContext.User.GetUsername();

            var bookings = await _bookingService.GetBookingsForPatientAsync(patientName);

             return Ok(bookings);
        }


        
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingToReturnDto>> GetBookingByIdForPatient(int id)
        {
            var patientName = HttpContext.User.GetUsername();

            var booking = await _bookingService.GetBookingByIdAsync(id, patientName);

            if (booking == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<BookingToReturnDto>(booking);
        }


         [HttpGet("appointmentType")]
        public async Task<ActionResult<IReadOnlyList<AppointmentType>>> GetAppointmentType()
        {
            return Ok(await _bookingService.GetAppointmentTypesAsync());
        }


    }}