using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("MedicalExpertises")]
    public class MedicalExpertise
    {
        public int Id { get; set; }
        public string Specialization { get; set; }
        public string Certifications { get; set; }
        public string Trainings { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}

