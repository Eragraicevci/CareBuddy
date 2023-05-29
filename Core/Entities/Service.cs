namespace Core.Entities
{
    public class Service : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public ServiceType ServiceType { get; set; }
        public int ServiceTypeId { get; set; }
        public Hospital Hospital { get; set; }
        public int HospitalId { get; set; }
    }
}
