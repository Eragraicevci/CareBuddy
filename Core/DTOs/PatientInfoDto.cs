using System.ComponentModel.DataAnnotations;
namespace Core.Dtos
{
    public class PatientInfoDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        [Required] public string ContactInfo { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string MedicalHistory { get; set; }
        [Required]
        public string CurrentMedication { get; set; }
        [Required]
        public string Symptoms { get; set; }
    }
}