namespace API.DTOs
{
    public class UserDto
    {
        public string Username { get; set; }

        public string Token { get; set; }

        public string Photo { get; set; }
        public string AnalysisResultFile { get; set; }

        public string KnownAs { get; set; }
    }
}
