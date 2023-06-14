namespace Core.DTOs
{
    public class BookingItemDto
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
    }
}
