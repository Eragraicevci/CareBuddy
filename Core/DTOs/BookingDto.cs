namespace Core.Dtos
{
    public class BookingDto
    {
        public string AppointmentId { get; set; }
        public int AppointmentTypeId { get; set; }
        public PatientInfoDto Info { get; set; }
    }
}