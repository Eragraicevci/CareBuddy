using Core.Dtos;

namespace Core.DTOs
{
    public class BookingDto
    {
        public string AppointmentId { get; set; }
        public int AppointmetTypeId { get; set; }
        public PatientInfoDto Info { get; set; }
    }
}
