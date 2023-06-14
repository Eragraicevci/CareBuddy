namespace Core.Entities.BookingAggregate
{
    public class AppointmentType : BaseEntity
    {
        public string ShortName { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

}