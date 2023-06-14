using Core.Entities.BookedAggregate;

namespace Core.Entities.BookingAggregate
{
    public class BookingItem : BaseEntity
    {
        public BookingItem()
        {
        }

        public BookingItem(ServiceItemBooked itemBooked, decimal price, int capacity)
        {
            ItemBooked = itemBooked;
            Price = price;
            Capacity = capacity;
        }

        public ServiceItemBooked ItemBooked { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
    }
}