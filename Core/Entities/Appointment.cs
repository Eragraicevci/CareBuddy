namespace Core.Entities
{
    public class Appointment
    {
        
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string Hospital { get; set; }
        public string Type { get; set; }
    }
}