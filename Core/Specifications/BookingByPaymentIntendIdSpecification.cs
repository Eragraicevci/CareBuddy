using Core.Entities.BookingAggregate;

namespace Core.Specifications
{
    public class BookingByPaymentIntendIdSpecification : BaseSpecification<Booking>
    {
        public BookingByPaymentIntendIdSpecification(string paymentIntentId) 
            : base(o => o.PaymentIntentId == paymentIntentId)
        {
        }
    }
}