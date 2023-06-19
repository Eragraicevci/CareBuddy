namespace Core.Entities
{
    public class PatientAppointment
    {
        public PatientAppointment() { }

        public PatientAppointment(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
        public List<Appointment> Items { get; set; } = new List<Appointment>();
         public int? AppointmentTypeId { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }
        public decimal AppointmentTypePrice  { get; set; }
    }
}
