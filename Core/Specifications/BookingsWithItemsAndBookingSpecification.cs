using Core.Entities.BookingAggregate;

namespace Core.Specifications
{
    public class BookingsWithItemsAndBookingSpecification: BaseSpecification<Booking>
    {
        public BookingsWithItemsAndBookingSpecification(string username) : base(o => o.PatientUsername == username)
        {
            AddInclude(o => o.BookingItems);
            AddInclude(o => o.AppointmentType);
            AddOrderByDescending(o => o.BookedDate);
        }

        public BookingsWithItemsAndBookingSpecification(int id, string username)
            : base(o => o.Id == id && o.PatientUsername == username)
        {
            AddInclude(o => o.BookingItems);
            AddInclude(o => o.AppointmentType);
        }
    }
}