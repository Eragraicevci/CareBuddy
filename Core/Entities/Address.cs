using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("Addresses")]
    public class Address
    {
        public int Id { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string StreetName { get; set; }
        public int ZipCode { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}