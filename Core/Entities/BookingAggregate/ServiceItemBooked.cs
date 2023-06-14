namespace Core.Entities.BookedAggregate
{
    public class ServiceItemBooked
    {
        public ServiceItemBooked()
        {
        }

        public ServiceItemBooked(int serviceItemId, string serviceName, string pictureUrl)
        {
            ServiceItemId = serviceItemId;
            ServiceName = serviceName;
            PictureUrl = pictureUrl;
        }

        public int ServiceItemId { get; set; }
        public string ServiceName { get; set; }
        public string PictureUrl { get; set; }
    }
}