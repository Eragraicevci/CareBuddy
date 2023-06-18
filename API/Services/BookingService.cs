using System.Text.RegularExpressions;
using API.Repositories.Interfaces;
using Core.Entities;
using Core.Entities.BookedAggregate;
using Core.Entities.BookingAggregate;
using Core.Interfaces;
using Core.Specifications;

namespace API.Services
{
    public class BookingService : IBookingService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Service> _serviceRepo;
        private readonly IGenericRepository<AppointmentType> _atRepo;

        public BookingService(
            IAppointmentRepository appointmentRepository,
            IUnitOfWork unitOfWork,
            IGenericRepository<Service> serviceRepo,
            IGenericRepository<AppointmentType> atRepo
        )
        {
            _atRepo = atRepo ?? throw new ArgumentNullException(nameof(atRepo));
            _unitOfWork = unitOfWork;
            _appointmentRepository = appointmentRepository;
            _serviceRepo = serviceRepo ?? throw new ArgumentNullException(nameof(serviceRepo));
        }

        public async Task<Booking> CreateBookingAsync(
            string patientUsername,
            int appointmentTypeId,
            string appointmentId,
            PatientInfo info
        )
        {
            var appointment = await _appointmentRepository.GetAppointmentAsync(appointmentId);

            var items = new List<BookingItem>();
            if (appointment != null && appointment.Items != null)
            {
                foreach (var item in appointment.Items)
                {
                    var serviceItem = await _serviceRepo.GetByIdAsync(item.Id);

                    if (serviceItem != null)
                    {
                        var itemBooked = new ServiceItemBooked(
                            serviceItem.Id,
                            serviceItem.Name,
                            serviceItem.PictureUrl
                        );
                        var bookingItem = new BookingItem(
                            itemBooked,
                            serviceItem.Price,
                            item.Capacity
                        );
                        items.Add(bookingItem);
                    }
                }
            }

            var appointmentType = await _atRepo.GetByIdAsync(appointmentTypeId);

            var subtotal = items.Sum(item => item.Price * item.Capacity);

            var booking = new Booking(items, patientUsername, info, appointmentType, subtotal);

            return booking;
        }

        public async Task<IReadOnlyList<AppointmentType>> GetAppointmentTypesAsync()
        {
            return await _unitOfWork.Repository<AppointmentType>().ListAllAsync();
        }

        public async Task<Booking> GetBookingByIdAsync(int id, string patientUsername)
        {
            var spec = new BookingsWithItemsAndBookingSpecification(id, patientUsername);

            return await _unitOfWork.Repository<Booking>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Booking>> GetBookingsForAsync(string patientUsername)
        {
            var spec = new BookingsWithItemsAndBookingSpecification(patientUsername);

            return await _unitOfWork.Repository<Booking>().ListAsync(spec);
        }

        public async Task<IReadOnlyList<Booking>> GetBookingsForPatientAsync(string patientUsername)
        {
            var spec = new BookingsWithItemsAndBookingSpecification(patientUsername);

            return await _unitOfWork.Repository<Booking>().ListAsync(spec);
        }
    }
}
