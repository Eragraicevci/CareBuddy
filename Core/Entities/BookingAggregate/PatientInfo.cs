namespace Core.Entities.BookingAggregate
{
    public class PatientInfo
    {
        public PatientInfo()
        {
            
        }
        public PatientInfo(string firstName, string lastName, string contactInfo, string address,
        string medicalHistory, string currentMedication, string symptoms)
        {
            FirstName = firstName;
            LastName = lastName;
            ContactInfo = contactInfo;
            Address = address;
            MedicalHistory = medicalHistory;
            CurrentMedication = currentMedication;
            Symptoms = symptoms;

        }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ContactInfo { get; set; }

        public string Address { get; set; }

        public string MedicalHistory { get; set; }

        public string CurrentMedication { get; set; }

        public string Symptoms { get; set; }
    }
}