
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("AnalysisResultFiles")]
    public class AnalysisResultFile
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;
        public string PublicId { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}