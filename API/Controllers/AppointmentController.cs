using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AppointmentController : BaseApiController
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentController(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<PatientAppointment>> GetAppointmentById(string id)
        {
            PatientAppointment appointment = await _appointmentRepository.GetAppointmentAsync(id);

            return Ok(appointment ?? new PatientAppointment(id));
        }

        [HttpPost]
        public async Task<ActionResult<PatientAppointment>> UpdateAppointment(PatientAppointment appointment)
        {
            var updatedAppointment = await _appointmentRepository.UpdateAppointmentAsync(appointment);

            return Ok(updatedAppointment);
        }

        [HttpDelete]
        public async Task DeleteAppointmentAsync(string id)
        {
            await _appointmentRepository.DeleteAppointmentAsync(id);
        }
    }
}
