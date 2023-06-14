using Core.Entities.BookingAggregate;

namespace Core.DTOs
{
    public class BookingToReturnDto
    {
        public int Id { get; set; }
        public string PatientUsername { get; set; }
        public DateTime BookedDate { get; set; }

        public PatientInfo Info { get; set; }
        public string AppointmentType { get; set; }
        public decimal AppointmentTypePrice { get; set; }

        public IReadOnlyList<BookingItemDto> BookingItems { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }

        public string Status { get; set; }
    }
}
