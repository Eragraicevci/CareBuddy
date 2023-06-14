using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Core.Entities;
using Core.Entities.BookingAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, 
            RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var userData = await File.ReadAllTextAsync("../Infrastructure/Data/UserSeedData.json");

            var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};

            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            var roles = new List<AppRole>
            {
                new AppRole{Name = "Pacient"},
                new AppRole{Name = "Admin"},
                new AppRole{Name = "Doctor"},
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
                user.UserName = user.UserName.ToLower();
                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, "Pacient");
            }

            var admin = new AppUser
            {
                UserName = "admin"
            };

            await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.AddToRolesAsync(admin, new[] {"Admin", "Doctor"});
        }


         public static async Task SeedAsync(DataContext context)
        {
            if (!context.Hospitals.Any())
            {
                var hospitalsData = File.ReadAllText("../Infrastructure/Data/SeedData/hospitals.json");
                var hospitals = JsonSerializer.Deserialize<List<Hospital>>(hospitalsData);
                context.Hospitals.AddRange(hospitals);
            }

            if (!context.ServiceTypes.Any())
            {
                var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ServiceType>>(typesData);
                context.ServiceTypes.AddRange(types);
            }

            if (!context.Services.Any())
            {
                var servicesData = File.ReadAllText("../Infrastructure/Data/SeedData/services.json");
                var services = JsonSerializer.Deserialize<List<Service>>(servicesData);
                context.Services.AddRange(services);
            }

             if (!context.AppointmentTypes.Any())
            {
                var appointmentTypesData = File.ReadAllText("../Infrastructure/Data/SeedData/appointmentTypes.json");
                var types = JsonSerializer.Deserialize<List<AppointmentType>>(appointmentTypesData);
                context.AppointmentTypes.AddRange(types);
            }

            if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        }
    }
}