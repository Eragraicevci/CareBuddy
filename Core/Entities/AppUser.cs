using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string KnownAs { get; set; }
        public string Name { get; set; }

        public string LastName { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public DateTime LastActive { get; set; } = DateTime.UtcNow;

        public string Gender { get; set; }

        public string Description { get; set; }

        public string LanguageSpoken { get; set; }

        public List<Photo> Photos { get; set; } = new();

        public List<AnalysisResultFile> AnalysisResultFiles { get; set; } = new();

        public List<MedicalExpertise> MedicalExpertises { get; set; } = new();
        public List<Address> Addresses { get; set; } = new();

        public List<UserLike> LikedByUsers { get; set; }

        public List<UserLike> LikedUsers { get; set; }

        public List<Message> MessagesSent { get; set; }
        public List<Message> MessagesReceived { get; set; }

        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
