using System.Text.Json;
using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly IDatabase _database;

        public AppointmentRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteAppointmentAsync(string appointmentId)
        {
            return await _database.KeyDeleteAsync(appointmentId);
        }


        public async Task<PatientAppointment> GetAppointmentAsync(string appointmentId)
        {
            var data = await _database.StringGetAsync(appointmentId);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<PatientAppointment>(data);
        }


        public async Task<PatientAppointment> UpdateAppointmentAsync(PatientAppointment appointment)
        {
            var created = await _database.StringSetAsync(
                appointment.Id,
                JsonSerializer.Serialize(appointment),
                TimeSpan.FromDays(30)
            );

            if (!created)
                return null;

            return await GetAppointmentAsync(appointment.Id);
        }
    }
}
