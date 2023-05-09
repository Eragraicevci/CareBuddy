using System;
using API.Extensions;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public string KnownAs { get; set; }
        public string Name { get; set; }

        public string LastName { get; set; }
        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

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
        
    }
}
