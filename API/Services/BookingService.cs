using API.Repositories.Interfaces;
using Core.Entities;
using Core.Entities.BookedAggregate;
using Core.Entities.BookingAggregate;
using Core.Interfaces;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class BookingService : IBookingService
    {
        private readonly IGenericRepository<Booking> _bookingRepo;
        private readonly IGenericRepository<Service> _serviceRepo;
        private readonly IAppointmentRepository _appointmentRepo;
        private readonly IGenericRepository<AppointmentType> _atRepo;

        public BookingService(
            IGenericRepository<Booking> bookingRepo,
            IGenericRepository<AppointmentType> atRepo,
            IGenericRepository<Service> serviceRepo,
            IAppointmentRepository appointmentRepo)
        {
            _atRepo = atRepo ?? throw new ArgumentNullException(nameof(atRepo));
            _appointmentRepo = appointmentRepo ?? throw new ArgumentNullException(nameof(appointmentRepo));
            _serviceRepo = serviceRepo ?? throw new ArgumentNullException(nameof(serviceRepo));
            _bookingRepo = bookingRepo ?? throw new ArgumentNullException(nameof(bookingRepo));
        }

        public async Task<Booking> CreateBookingAsync(string patientUsername, int appointmentTypeId, string appointmentId, PatientInfo info)
        {
            var appointment = await _appointmentRepo.GetAppointmentAsync(appointmentId);

            var items = new List<BookingItem>();
            foreach (var item in appointment.Items)
            {
                var serviceItem = await _serviceRepo.GetByIdAsync(item.Id);

                if (serviceItem != null)
                {
                    var itemBooked = new ServiceItemBooked(serviceItem.Id, serviceItem.Name, serviceItem.PictureUrl);
                    var bookingItem = new BookingItem(itemBooked, serviceItem.Price, item.Capacity);
                    items.Add(bookingItem);
                }
            }

            var appointmentType = await _atRepo.GetByIdAsync(appointmentTypeId);

            var subtotal = items.Sum(item => item.Price * item.Capacity);

            var booking = new Booking(items, patientUsername, info, appointmentType, subtotal);

            return booking;
        }

        public Task<IReadOnlyList<AppointmentType>> GetAppointmentTypesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Booking> GetBookingByIdAsync(int id, string patientUsername)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Booking>> GetBookingsForUserAsync(string patientUsername)
        {
            throw new NotImplementedException();
        }
    }
}
