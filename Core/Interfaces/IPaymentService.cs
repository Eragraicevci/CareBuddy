using Core.Entities;
using Core.Entities.BookingAggregate;

namespace Core.Interfaces
{
    public interface IPaymentService
    {
        Task<PatientAppointment> CreateOrUpdatePaymentIntent(string appointmentId);
        Task<Booking> UpdateBookingPaymentSucceeded(string paymentIntentId);
        Task<Booking> UpdateBookingPaymentFailed(string paymentIntentId);
    }
}