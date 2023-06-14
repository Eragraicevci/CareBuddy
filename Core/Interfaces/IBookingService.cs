using Core.Entities.BookingAggregate;

namespace Core.Interfaces
{
    public interface IBookingService
    {
        Task<Booking> CreateBookingAsync(string patientUsername, int appointmentType, 
        string appointmentId, PatientInfo info);
        Task<IReadOnlyList<Booking>> GetBookingsForUserAsync(string patientUsername);
        Task<Booking> GetBookingByIdAsync(int id, string patientUsername);
        Task<IReadOnlyList<AppointmentType>> GetAppointmentTypesAsync();
    }
}