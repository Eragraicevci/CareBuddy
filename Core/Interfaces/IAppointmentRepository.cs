using Core.Entities;

namespace Core.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<PatientAppointment> GetAppointmentAsync(string appointmentId);

        Task<PatientAppointment> UpdateAppointmentAsync(PatientAppointment appointment);

        Task<bool> DeleteAppointmentAsync(string appointmentId);
    }
}
