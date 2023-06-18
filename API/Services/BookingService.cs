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

        public BookingService(IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Booking> CreateBookingAsync(
            string patientUsername,
            int appointmentTypeId,
            string appointmentId,
            PatientInfo info
        )
        {
            // get appointment from repo
            var appointment = await _appointmentRepository.GetAppointmentAsync(appointmentId);

            // get items from the product repo
            var items = new List<BookingItem>();
            foreach (var item in appointment.Items)
            {
                var serviceItem = await _unitOfWork.Repository<Service>().GetByIdAsync(item.Id);
                var itemBooked = new ServiceItemBooked(
                    serviceItem.Id,
                    serviceItem.Name,
                    serviceItem.PictureUrl
                );
                var bookItem = new BookingItem(itemBooked, serviceItem.Price, item.Capacity);
                items.Add(bookItem);
            }

            // get  appointment type from repo
            var appointmentType = await _unitOfWork
                .Repository<AppointmentType>()
                .GetByIdAsync(appointmentTypeId);

            // calc subtotal
            var subtotal = items.Sum(item => item.Price * item.Capacity);

            // create book
            var book = new Booking(items, patientUsername, info, appointmentType, subtotal);
            _unitOfWork.Repository<Booking>().Add(book);

            // save to db
            var result = await _unitOfWork.Completee();

            if (result <= 0)
                return null;

            // delete appointment
            await _appointmentRepository.DeleteAppointmentAsync(appointmentId);

            return book;
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
