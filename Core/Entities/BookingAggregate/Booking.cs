using Core.Entities.BookingAggregate;

namespace Core.Entities.BookingAggregate
{
    public class Booking : BaseEntity
    {
        public Booking() { }

        public Booking(
            IReadOnlyList<BookingItem> bookingItems,
            string patientUsername,
            PatientInfo info,
            AppointmentType appointmentType,
            decimal subtotal
            // string paymentIntentId
        )
        {
            PatientUsername = patientUsername;
            Info = info;
            AppointmentType = appointmentType;
            BookingItems = bookingItems;
            Subtotal = subtotal;
            // PaymentIntentId = paymentIntentId;
        }

        public string PatientUsername { get; set; }
        public DateTime BookedDate { get; set; } = DateTime.UtcNow;

        public PatientInfo Info { get; set; }
        public AppointmentType AppointmentType { get; set; }
        public IReadOnlyList<BookingItem> BookingItems { get; set; }
        public decimal Subtotal { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;
        public string PaymentIntentId { get; set; }

        public decimal GetTotal()
        {
            return Subtotal + AppointmentType.Price;
        }
    }
}
